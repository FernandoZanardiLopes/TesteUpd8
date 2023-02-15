using Microsoft.EntityFrameworkCore;
using TesteUPD8.Models;

namespace TesteUPD8.DataBase
{
    public class Context : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DataBaseConnection.ConnectionString);
        }
    }
}
