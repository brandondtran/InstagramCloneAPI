using InstagramCloneAPI.Dtos;

namespace InstagramCloneAPI.Services
{
    public interface IAuthService
    {
        Task<(bool IsSuccess, string Token, string ErrorMessage)> LoginAsync(LoginDto loginRequest);
        Task LogoutAsync();
    }
}

