namespace InterviewPrep.Domain.Entities;

public class UserAnswer
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; } = string.Empty;

    public int? Score { get; set; }

    public string? Feedback { get; set; }

    public int AttemptNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    // public User User { get; set; } = null!;

    // public Question Question { get; set; } = null!;
}