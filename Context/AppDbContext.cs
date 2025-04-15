using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Models;

namespace ProjetoClinica.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("clinica");

            modelBuilder.Entity<Paciente>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");
        }
    }
}
