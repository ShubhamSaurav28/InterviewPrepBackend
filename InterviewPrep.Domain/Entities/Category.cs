namespace InterviewPrep.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? IconUrl { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    // public ICollection<Question> Questions { get; set; }
    //     = new List<Question>();

    // public ICollection<UserSkill> UserSkills { get; set; }
    //     = new List<UserSkill>();

    // public ICollection<UserProgress> UserProgresses { get; set; }
    //     = new List<UserProgress>();
}