using Microsoft.EntityFrameworkCore;
using testconnect.Models;

namespace testconnect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<storepassword> storepasswords { get; set; } = default;
    }
}
