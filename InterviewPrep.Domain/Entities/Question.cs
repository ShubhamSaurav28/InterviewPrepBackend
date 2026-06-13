namespace InterviewPrep.Domain.Entities;

public class Question
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Difficulty { get; set; } = string.Empty;

    public string QuestionType { get; set; } = string.Empty;

    public string? ExpectedAnswer { get; set; }

    public string? Source { get; set; }

    public string? Tags { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    // public Category Category { get; set; } = null!;

    // public ICollection<UserAnswer> UserAnswers { get; set; }
    //     = new List<UserAnswer>();

    // public ICollection<Bookmark> Bookmarks { get; set; }
    //     = new List<Bookmark>();
}