using System.Security.Claims;
using InterviewPrep.Infrastructure.Data;
using InterviewPrep.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/bookmarks")]
public class BookmarksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookmarksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookmarks()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var bookmarks = await _context.Bookmarks
            .Where(b => b.UserId == Guid.Parse(userId) && !b.IsDeleted)
            .Join(
                _context.Questions,
                b => b.QuestionId,
                q => q.Id,
                (b, q) => new
                {
                    BookmarkId = b.Id,
                    QuestionId = q.Id,
                    q.Title,
                    q.Description,
                    q.Difficulty,
                    q.QuestionType
                })
            .ToListAsync();

        return Ok(bookmarks);
    }

    [HttpPost("{questionId}")]
    public async Task<IActionResult> AddBookmark(Guid questionId)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var existingBookmark = await _context.Bookmarks
            .FirstOrDefaultAsync(b =>
                b.UserId == Guid.Parse(userId)
                && b.QuestionId == questionId);

        if (existingBookmark != null)
        {
            if (existingBookmark.IsDeleted)
            {
                existingBookmark.IsDeleted = false;
                existingBookmark.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok("Bookmark restored");
            }

            return BadRequest("Question already bookmarked");
        }

        var bookmark = new Bookmark
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse(userId),
            QuestionId = questionId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        _context.Bookmarks.Add(bookmark);

        await _context.SaveChangesAsync();

        return Ok("Bookmark Added");
    }

    [HttpDelete("{questionId}")]
    public async Task<IActionResult> DeleteBookmark(Guid questionId)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var bookmark = await _context.Bookmarks
            .FirstOrDefaultAsync(b =>
                b.UserId == Guid.Parse(userId)
                && b.QuestionId == questionId
                && !b.IsDeleted);

        if (bookmark == null)
            return NotFound("Bookmark not found");

        bookmark.IsDeleted = true;
        bookmark.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok("Bookmark removed");
    }
}