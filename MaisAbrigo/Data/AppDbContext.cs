using MaisAbrigo.Model;
using Microsoft.EntityFrameworkCore;

namespace MaisAbrigo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Abrigo> Abrigos { get; set; }
    }
}
