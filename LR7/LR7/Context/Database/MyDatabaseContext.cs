using LR7.Models;
using Microsoft.EntityFrameworkCore;

namespace LR7.Context.Database {
    public class MyDatabaseContext : DbContext {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options) {

        }

        public DbSet<User> Users { get; set; }
    }
}
