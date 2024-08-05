using SmartCardSystem.DTOs;
using SmartCardSystem.Responses;
using SmartCardSystem.State;
using static SmartCardSystem.Responses.CustomResponses;
namespace SmartCardSystem.Services
{
    public class AccountService(HttpClient _httpClient) : IAccountService
    {
        private const string BaseUrl = "api/account";
        
        

        private async Task GetRefreshToken()
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/refresh-token",new UserSession() { JWTToken = Constants.JWTToken!});
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            Constants.JWTToken = result!.JWTToken;
        }

        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", model);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }
        
        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
            return result!;
        }
        
        public async Task<LoginResponse> RefreshToken(UserSession model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/refresh-token", model);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }
    }
}
