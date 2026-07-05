using System.Security.Claims;
using InterviewPrep.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterviewPrep.Application.DTOs.Users;

namespace InterviewPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id.ToString() == userId);

        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Email,
            user.TargetRole,
            user.ExperienceLevel,
            user.Country,
            user.Timezone
        });
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe(
        UpdateProfileRequest request)
    {
        var userId =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Id.ToString() == userId);

        if (user == null)
            return NotFound();

        user.Name =
            request.Name ?? user.Name;

        user.TargetRole =
            request.TargetRole ?? user.TargetRole;

        user.ExperienceLevel =
            request.ExperienceLevel ?? user.ExperienceLevel;

        user.Country =
            request.Country;

        user.Timezone =
            request.Timezone;

        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Profile updated successfully"
        });
    }
}