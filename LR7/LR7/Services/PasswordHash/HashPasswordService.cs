using System.Text;

namespace LR7.Services.PasswordHash {
    public class HashPasswordService : IPasswordHashService {
        public string HashPassword (string password) {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var hashedPass = BCrypt.Net.BCrypt.HashPassword(password, salt);
            
            return hashedPass;
        }

        public bool VerifyPassword(string password, string hashedPassword) { 
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
