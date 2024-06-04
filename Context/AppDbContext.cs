using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTE_Migration.Areas.Admin.Models;
using MyTE_Migration.Models;

namespace MyTE_Migration.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<WBS> WBS { get; set; }
        public DbSet<HorasTrabalhadas> HorasTrabalhadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HorasTrabalhadas>()
                .HasOne(h => h.funcionario)
                .WithMany(f => f.horasTrabalhadas)
                .HasForeignKey(h => h.Funcionario_ID);

            base.OnModelCreating(modelBuilder);
        }

    }
   
}
