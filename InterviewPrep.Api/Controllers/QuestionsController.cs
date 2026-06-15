using InterviewPrep.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterviewPrep.Application.DTOs.Questions;

namespace InterviewPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public QuestionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions([FromQuery] QuestionQueryRequest request)
    {
        request.Page = Math.Max(1, request.Page);

        request.PageSize =
            Math.Min(Math.Max(1, request.PageSize), 100);

        var query = _context.Questions
            .Where(q => !q.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            query = query.Where(q =>
                q.Title.ToLower()
                .Contains(request.Keyword.ToLower()));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(q =>
                q.CategoryId == request.CategoryId.Value);
        }

        var totalCount = await query.CountAsync();

        var questions = await query
            .OrderBy(q => q.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return Ok(new
        {
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize,
            Items = questions
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionById(Guid id)
    {
        var question = await _context.Questions
            .FirstOrDefaultAsync(q =>
                q.Id == id &&
                !q.IsDeleted);

        if (question == null)
            return NotFound();

        return Ok(question);
    }
}