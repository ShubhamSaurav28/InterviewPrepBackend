using System.Security.Claims;
using InterviewPrep.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/progress")]
public class ProgressController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProgressController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProgress()
    {
        var userId = User.FindFirst(
            ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var userGuid = Guid.Parse(userId);

        var totalAnswers = await _context.UserAnswers
            .CountAsync(x =>
                x.UserId == userGuid &&
                !x.IsDeleted);

        var totalBookmarks = await _context.Bookmarks
            .CountAsync(x =>
                x.UserId == userGuid &&
                !x.IsDeleted);

        var categoriesAttempted = await _context.UserAnswers
            .Where(x =>
                x.UserId == userGuid &&
                !x.IsDeleted)
            .Join(
                _context.Questions,
                ua => ua.QuestionId,
                q => q.Id,
                (ua, q) => q.CategoryId)
            .Distinct()
            .CountAsync();

        var latestAnswer = await _context.UserAnswers
            .Where(x =>
                x.UserId == userGuid &&
                !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        return Ok(new
        {
            TotalAnswers = totalAnswers,
            TotalBookmarks = totalBookmarks,
            CategoriesAttempted = categoriesAttempted,
            LatestAnswerDate = latestAnswer
        });
    }
}