using SmartCardSystem.DTOs;
using static SmartCardSystem.Responses.CustomResponses;
namespace SmartCardSystem.Repository
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> LoginAsync(LoginDTO model);
        LoginResponse RefreshToken(UserSession session);
    }
}
