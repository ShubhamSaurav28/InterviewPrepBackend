namespace InterviewPrep.Application.DTOs.Questions;

public class QuestionQueryRequest
{
    public string? Keyword { get; set; }

    public Guid? CategoryId { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}