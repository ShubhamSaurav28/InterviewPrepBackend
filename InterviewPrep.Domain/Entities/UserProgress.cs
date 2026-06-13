namespace InterviewPrep.Domain.Entities;

public class UserProgress
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }

    public int AttemptedQuestions { get; set; }

    public int CompletedQuestions { get; set; }

    public decimal? AverageScore { get; set; }

    public DateTime? LastAttemptedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    // public User User { get; set; } = null!;

    // public Category Category { get; set; } = null!;
}