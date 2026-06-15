using InterviewPrep.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace InterviewPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();

        return Ok(categories);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetQuestionsByCategory(Guid categoryId)
    {
        var questions = await _context.Questions
            .Where(q => q.CategoryId == categoryId)
            .ToListAsync();

        return Ok(questions);
    }

    [HttpGet("{categoryId}/questions")]
    public async Task<IActionResult> GetCategoryQuestions(
        Guid categoryId)
    {
        var questions = await _context.Questions
            .Where(q =>
                q.CategoryId == categoryId
                && !q.IsDeleted)
            .OrderBy(q => q.CreatedAt)
            .ToListAsync();

        return Ok(questions);
    }
}