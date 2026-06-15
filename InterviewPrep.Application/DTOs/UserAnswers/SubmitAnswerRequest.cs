namespace InterviewPrep.Application.DTOs.UserAnswers;

public class SubmitAnswerRequest
{
    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; } = string.Empty;
}