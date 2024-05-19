using LR7.Models;
using LR7.Models.Auth;
using LR7.Services.PasswordHash;
using LR7.Services.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LR7.Services.Auth {
    public class AuthService : IAuthService {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHashService _passwordHashService;

        public AuthService(IConfiguration configuration, IPasswordHashService passwordHashService, IUserService userService) {
            _configuration = configuration;
            _passwordHashService = passwordHashService;
            _userService = userService;
        }

        public async Task<User> LoginAsync(string email, string password) {
            var users = await _userService.GetUsersAsync();
            var user = users.FirstOrDefault(u => u.Email == email);
            if (user != null && _passwordHashService.VerifyPassword(password, user.Password)) {
                user.LastLoginDate = DateTime.Now;
                user.FailedLoginAttempts = 0;
                await _userService.UpdateUserAsync(user); // Update last login date and failed attempts
                return user;
            } else {
                if (user != null) {
                    user.FailedLoginAttempts++;
                    await _userService.UpdateUserAsync(user); // Update failed attempts
                }
                return null;
            }
        }

        public async Task<User> RegistrateAsync(User user) {
            user.Password = _passwordHashService.HashPassword(user.Password);
            var createdUser = await _userService.CreateUserAsync(user);
            return createdUser;
        }

        public string GenerateToken(User user) {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: cred,
                expires: DateTime.Now.AddDays(1)
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
