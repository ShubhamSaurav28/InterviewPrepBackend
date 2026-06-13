using InterviewPrep.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.Api.Controllers;

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
}