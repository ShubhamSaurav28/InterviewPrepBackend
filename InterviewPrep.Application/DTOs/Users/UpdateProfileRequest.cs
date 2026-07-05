namespace InterviewPrep.Application.DTOs.Users;

public class UpdateProfileRequest
{
    public string? Name { get; set; }

    public string? TargetRole { get; set; }

    public string? ExperienceLevel { get; set; }

    public string? Country { get; set; }

    public string? Timezone { get; set; }
}