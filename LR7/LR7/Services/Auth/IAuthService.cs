using LR7.Models;
using LR7.Models.Auth;
using LR7.Services.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LR7.Services.Auth {
    public interface IAuthService {
        Task<User> LoginAsync(string email, string password);
        Task<User> RegistrateAsync(User user);
        string GenerateToken(User user);
    }
}