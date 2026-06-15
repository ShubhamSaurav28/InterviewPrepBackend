namespace InterviewPrep.Application.DTOs.Auth;

public class RegisterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string TargetRole { get; set; } = string.Empty;
    public string ExperienceLevel { get; set; } = string.Empty;
}
