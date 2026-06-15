using InterviewPrep.Application.DTOs.Auth;

namespace InterviewPrep.Api.Interfaces;
public interface IAuthService
{
    Task<AuthResponse> Register(RegisterRequest request);
    Task<AuthResponse> Login(LoginRequest request);
}
