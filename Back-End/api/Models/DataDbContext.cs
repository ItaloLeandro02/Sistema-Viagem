using Microsoft.EntityFrameworkCore;
using api.Views;
using System.Collections.Generic;

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
        public DbSet<ViagemDespesa> ViagemDespesa { get; set; }
        public DbSet<DashboardFaturamento> Faturamento { get; set; }
        public DbSet<DashboardComissao> Comissao { get; set; }
        public DbSet<DashboardFaturamentoUf> FaturamentoUf { get; set; }
        public DbSet<DashboardMapaBrasil> MapaBrasil { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Viagem>()
                .HasMany(p => p.combustivel)
                .WithOne()
                .HasForeignKey(p => p.viagem);

             modelBuilder.Entity<Viagem>()
                .HasMany(p => p.despesas)
                .WithOne();
                //.HasForeignKey(p => p.Id);    

                modelBuilder.Ignore<ViagemDespesa>();

        }
    }
}