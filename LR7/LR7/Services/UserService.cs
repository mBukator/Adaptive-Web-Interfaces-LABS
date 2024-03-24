using LR7.Models;

namespace LR7.Services {
    public class UserService : IUserService {

        private static readonly List<User> _users = new List<User> {
            new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Password = "ngfzzj;l124", PhoneNumber = "+380632345678", BirthDate = new DateTime(1980, 1, 1) },
            new User { Id = 2, Name = "Marc Noir", Email = "noname@example.com", Password = "asafdsfsd", PhoneNumber = "+380506789154", BirthDate = new DateTime(1981, 2, 2) },
            new User { Id = 3, Name = "Johna Hill", Email = "myweightismorethanearth@example.com", Password = "sg4322fs", PhoneNumber = "+380966545784", BirthDate = new DateTime(1982, 3, 1) },
            new User { Id = 4, Name = "Dwayne Johnson", Email = "justrock@example.com", Password = "vaafgegvsgvsvs", PhoneNumber = "+380666836784", BirthDate = new DateTime(1983, 4, 1) },
            new User { Id = 5, Name = "Robert Downey Jr", Email = "playboyMillionaire@example.com", Password = "fs1312", PhoneNumber = "+380686412359", BirthDate = new DateTime(1984, 5, 1) },
            new User { Id = 6, Name = "Cillian Murphy", Email = "sigmaGigaChad@example.com", Password = "a;kcxuoe", PhoneNumber = "+380637894561", BirthDate = new DateTime(1985, 6, 1) },
            new User { Id = 7, Name = "Travis Scott", Email = "justrapper@example.com", Password = "124123", PhoneNumber = "+380637539514", BirthDate = new DateTime(1986, 1, 7) },
            new User { Id = 8, Name = "Leonardo da Vinci", Email = "risuyuPoKaifu@example.com", Password = "qwfdsseq3", PhoneNumber = "+38096852963", BirthDate = new DateTime(1987, 8, 1) },
            new User { Id = 9, Name = "Boris Johnson", Email = "yaBorisYaLoveUkraine@example.com", Password = "vsxvzq13421543", PhoneNumber = "+380506791784", BirthDate = new DateTime(1988, 9, 1) },
            new User { Id = 10, Name = "Homeless Yasha from Mykolaiv", Email = "iamhomeless@example.com", Password = "dvsdgwrwerwer", PhoneNumber = "+380936624874", BirthDate = new DateTime(1989, 10, 1) },
        };

        public async Task<IEnumerable<User>> GetUsersAsync() {
            try {
                return await Task.FromResult(_users);
            } catch (Exception ex) {
                Console.WriteLine($"Error getting user. Exception: {ex.Message}");
                return null;

            }
        }

        public async Task<User> GetUserByIdAsync(int id) {
            try {
                return await Task.FromResult(_users.FirstOrDefault(x => x.Id == id));
            } catch (Exception ex) {
                Console.WriteLine($"Error getting user by ID: {id}. Exception: {ex.Message}");
                return null; 
            }
        }

        public async Task CreateUserAsync(User user) {
            try {
                _users.Add(user);
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error creating user: {user.Name}. Exception: {ex.Message}");
            }

        }


        public async Task UpdateUserAsync(User user) {
            try {
                var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null) {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.BirthDate = user.BirthDate;
                }
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error updating user. Exception: {ex.Message}");
            }
        }
         
        public async Task DeleteUserAsync(int id) {
            try {
                var user = _users.FirstOrDefault(x => x.Id == id);
                if (user != null) {
                    _users.Remove(user);
                }
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error deleting user. Exception: {ex.Message}");
            }
        }
    }
}
