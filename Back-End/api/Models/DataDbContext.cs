using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base (options) 
        { }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<CidadeIbge> CidadeIbge { get; set; }
        public DbSet<Viagem> Viagem { get; set; }
    }
}