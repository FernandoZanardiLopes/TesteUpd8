using APIupd8.Model;
using Microsoft.EntityFrameworkCore;

namespace APIupd8.DataBase
{
    public class _Context : DbContext
    {
            public DbSet<Cliente> Cliente { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(DataBaseConnection.ConnectionString);
            }
        }
}
