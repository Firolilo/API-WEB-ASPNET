using Juan.Models;
using Microsoft.EntityFrameworkCore;

namespace Juan.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public DbSet<User> Usuarios { get; set; }
    }
}
