using Sat.Recruitment.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Sat.Recruitment.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local)\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}
