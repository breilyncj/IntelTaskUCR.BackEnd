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

        public DbSet<ETareas> T_Tareas { get; set; } 

        public DbSet<ERoles> T_Roles { get; set; }

        public DbSet<EOficinas> T_Oficinas { get; set; }
        
        public DbSet<EFrecuenciaRecordatorio> T_Frecuecia_Recordatorio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EDemo>().ToTable("T_Demo");
            modelBuilder.Entity<EDemo>().HasKey(d => d.TN_Codigo);

            modelBuilder.Entity<EUsuarios>().ToTable("T_Usuarios");
            modelBuilder.Entity<EUsuarios>().HasKey(d => d.CN_Id_usuario);

            modelBuilder.Entity<ETareas>().ToTable("T_Tareas");
            modelBuilder.Entity<ETareas>().HasKey(d => d.CN_Id_tarea);

            modelBuilder.Entity<ERoles>().ToTable("T_Roles");
            modelBuilder.Entity<ERoles>().HasKey(d => d.CN_Id_rol);

            modelBuilder.Entity<EOficinas>().ToTable("T_Oficinas");
            modelBuilder.Entity<EOficinas>().HasKey(d => d.CN_Codigo_oficina);

            modelBuilder.Entity<EFrecuenciaRecordatorio>().ToTable("T_Frecuencia_Recordatorio");
            modelBuilder.Entity<EFrecuenciaRecordatorio>().HasKey(d => d.CN_Id_recordatorio);
        }
    }
}
