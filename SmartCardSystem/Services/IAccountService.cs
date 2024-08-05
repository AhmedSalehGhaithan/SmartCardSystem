using SmartCardSystem.DTOs;
using static SmartCardSystem.Responses.CustomResponses;
namespace SmartCardSystem.Services
{
    public interface IAccountService
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> LoginAsync(LoginDTO model);
        Task<LoginResponse> RefreshToken(UserSession model);
    }
}
