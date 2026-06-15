using System.Security.Claims;
using InterviewPrep.Application.DTOs.UserAnswers;
using InterviewPrep.Domain.Entities;
using InterviewPrep.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/useranswers")]
public class UserAnswersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserAnswersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitAnswer(
        SubmitAnswerRequest request)
    {
        var userId = User.FindFirst(
            ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var previousAttempts = await _context.UserAnswers
            .CountAsync(x =>
                x.UserId == Guid.Parse(userId)
                && x.QuestionId == request.QuestionId);

        var answer = new UserAnswer
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse(userId),
            QuestionId = request.QuestionId,
            AnswerText = request.AnswerText,
            AttemptNumber = previousAttempts + 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        _context.UserAnswers.Add(answer);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Answer submitted",
            AttemptNumber = answer.AttemptNumber
        });
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyAnswers()
    {
        var userId = User.FindFirst(
            System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var answers = await _context.UserAnswers
            .Where(a =>
                a.UserId == Guid.Parse(userId)
                && !a.IsDeleted)
            .Join(
                _context.Questions,
                a => a.QuestionId,
                q => q.Id,
                (a, q) => new
                {
                    AnswerId = a.Id,
                    QuestionId = q.Id,
                    QuestionTitle = q.Title,
                    a.AnswerText,
                    a.Score,
                    a.Feedback,
                    a.AttemptNumber,
                    a.CreatedAt
                })
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(answers);
    }
}