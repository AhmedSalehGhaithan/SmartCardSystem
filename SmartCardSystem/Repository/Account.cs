using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartCardSystem.Data;
using SmartCardSystem.DTOs;
using SmartCardSystem.Models;
using SmartCardSystem.State;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SmartCardSystem.Responses.CustomResponses;
namespace SmartCardSystem.Repository
{
    public class Account(IConfiguration _config, AppDbContext _context) : IAccount
    {
        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser == null) return new LoginResponse(false, "User is not found");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, findUser!.Password))
                return new LoginResponse(false, "Email/Password is not valid");

            string jwtToken = GenerateToken(findUser);
            return new LoginResponse(true, "Login successfully", jwtToken);
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
             List<ApplicationUser> applicationUsers = await _context.Users.ToListAsync();

            var findUser = await GetUser(model.Email);
            if (findUser != null) return new RegistrationResponse(false, "User Already Exist");

            model.Role = (applicationUsers.Count <= 0) ? "Admin" : "User";

            _context.Users.Add(
                new ApplicationUser()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Role = model.Role,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                });

            await _context.SaveChangesAsync();
            return new RegistrationResponse(true,"User Created Successfully");
        }

        private async Task<ApplicationUser> GetUser(string email) => await _context.Users.FirstOrDefaultAsync(_e => _e.Email == email);

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role!),
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public LoginResponse RefreshToken(UserSession session)
        {
            CustomUserClaims customUserClaims = DecryptJWTService.DecryptToken(session.JWTToken);
            if (customUserClaims == null) return new LoginResponse(false, "Incorrect token");
            string newToken = GenerateToken(new ApplicationUser()
            {
                Name = customUserClaims.Name, Email = customUserClaims.Email,
            });
            return new LoginResponse(true, "New Token", newToken);
        }
    }
}
