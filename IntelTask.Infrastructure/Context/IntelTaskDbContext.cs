using IntelTaskUCR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntelTaskUCR.Infrastructure.Context
{
    public class IntelTaskDbContext : DbContext
    {
        public IntelTaskDbContext(DbContextOptions <IntelTaskDbContext> options) :  base(options) { 
        }

        public DbSet<EDemo> T_Demo { get; set; } 

        public DbSet<EUsuarios> T_Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EDemo>().ToTable("T_Demo");
            modelBuilder.Entity<EDemo>().HasKey(d => d.TN_Codigo);

            modelBuilder.Entity<EUsuarios>().ToTable("T_Usuarios");
            modelBuilder.Entity<EUsuarios>().HasKey(d => d.CN_Id_usuario);
        }
    }
}
