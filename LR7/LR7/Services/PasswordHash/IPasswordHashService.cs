namespace LR7.Services.PasswordHash {
    public interface IPasswordHashService {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
