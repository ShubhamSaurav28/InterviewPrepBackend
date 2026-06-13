namespace InterviewPrep.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? TargetRole { get; set; }

    public string? ExperienceLevel { get; set; }

    public string? ProfileImageUrl { get; set; }

    public string? Country { get; set; }

    public string? Timezone { get; set; }

    public bool IsEmailVerified { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    // public ICollection<UserAnswer> UserAnswers { get; set; }
    //     = new List<UserAnswer>();

    // public ICollection<Bookmark> Bookmarks { get; set; }
    //     = new List<Bookmark>();

    // public ICollection<UserSkill> UserSkills { get; set; }
    //     = new List<UserSkill>();

    // public ICollection<UserProgress> UserProgresses { get; set; }
    //     = new List<UserProgress>();
}