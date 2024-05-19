using LR7.Context.Database;
using LR7.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LR7.Services.Users {
    public class UserService : IUserService {
        private readonly MyDatabaseContext _context;
        public UserService(MyDatabaseContext context) {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() {
            try {
                return await _context.Users.ToListAsync();
            } catch (Exception ex) {
                Console.WriteLine($"Error getting user. Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<User> GetUserByIdAsync(int id) {
            try {
                return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            } catch (Exception ex) {
                Console.WriteLine($"Error getting user by ID: {id}. Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<User> CreateUserAsync(User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task UpdateUserAsync(User user) {
            try {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                Console.WriteLine($"Error updating user. Exception: {ex.Message}");
            }
        }

        public async Task DeleteUserAsync(int id) {
            try {
                var user = await _context.Users.FindAsync(id);
                if (user != null) {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error deleting user. Exception: {ex.Message}");
            }
        }
    }
}
