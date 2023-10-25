using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess.Models;
using WorketHealth.DataAccess.Models.Fecha;
using WorketHealth.DataAccess.Models.Personal;
using WorketHealth.DataAccess.Models.Registros;
using WorketHealth.Domain.Entities;

namespace WorketHealth.DataAccess {
    public class WorketHealthContext : IdentityDbContext<AppUsuario>{

        public WorketHealthContext(DbContextOptions<WorketHealthContext> options) : base(options)
        {

        }        
        public DbSet<Menu> Menus { get; set; }
        public DbSet<UserMenuAccess> UserMenuAccess { get; set; }
        public DbSet<Anho> Anho { get; set; }

        //Registro F_SEG_19 tablas
        public DbSet<Personal> Personal { get; set; }
        public DbSet<SeguimientoMedico> SeguimientoMedicos { get; set; }
        public DbSet<TipoExamen> TipoExamenes { get; set; }
        public DbSet<Aptitud> Aptitudes { get; set; }
        public DbSet<EnfermedadComun> EnfermedadesComunes { get; set; }
        public DbSet<EnfermedadRelacionadaTrabajo> EnfermedadesRelacionadasTrabajo { get; set; }
        public DbSet<EnfermedadProfesional> EnfermedadesProfesionales { get; set; }
        public DbSet<SeguimientoEnfermedad> SeguimientoEnfermedad { get; set; }
        public DbSet<SeguimientoEnfermedadTrabajo> SeguimientoEnfermedadTrabajo { get; set; }
        public DbSet<SeguimientoEnfermedadProfesional> SeguimientoEnfermedadProfesional { get; set; }

        //

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMenuAccess>()
                .HasKey(uma => new { uma.UserId, uma.MenuId });

            modelBuilder.Entity<UserMenuAccess>()
                .HasOne(uma => uma.User)
                .WithMany(u => u.UserMenuAccess)
                .HasForeignKey(uma => uma.UserId);

            modelBuilder.Entity<UserMenuAccess>()
                .HasOne(uma => uma.Menu)
                .WithMany(m => m.UserMenuAccess)
                .HasForeignKey(uma => uma.MenuId);

            //Registro F_SEG_19 tablas

            // Configuración de la relación muchos a muchos entre SeguimientoMedico y EnfermedadComun
            modelBuilder.Entity<SeguimientoEnfermedad>()
                .HasKey(se => new { se.SeguimientoMedicoId, se.EnfermedadComunId });

            modelBuilder.Entity<SeguimientoEnfermedad>()
                .HasOne(se => se.SeguimientoMedico)
                .WithMany(sm => sm.Enfermedades)
                .HasForeignKey(se => se.SeguimientoMedicoId);

            modelBuilder.Entity<SeguimientoEnfermedad>()
                .HasOne(se => se.EnfermedadComun)
                .WithMany(ec => ec.SeguimientoMedicos)
                .HasForeignKey(se => se.EnfermedadComunId);

            // Configuración de la relación uno a uno entre SeguimientoMedico y TipoExamen
            modelBuilder.Entity<SeguimientoMedico>()
                .HasOne(sm => sm.TipoExamen)
                .WithMany()
                .HasForeignKey(sm => sm.ID_TIPO_EXAMEN);

            // Configuración de la relación uno a uno entre SeguimientoMedico y Aptitud
            modelBuilder.Entity<SeguimientoMedico>()
                .HasOne(sm => sm.Aptitud)
                .WithMany()
                .HasForeignKey(sm => sm.ID_SEG_APT);

            // Configuración de la relación muchos a muchos entre SeguimientoMedico y EnfermedadRelacionadaTrabajo
            modelBuilder.Entity<SeguimientoEnfermedadTrabajo>()
                .HasKey(se => new { se.SeguimientoMedicoId, se.EnfermedadRelacionadaTrabajoId });

            modelBuilder.Entity<SeguimientoEnfermedadTrabajo>()
                .HasOne(se => se.SeguimientoMedico)
                .WithMany(sm => sm.EnfermedadesTrabajo)
                .HasForeignKey(se => se.SeguimientoMedicoId);

            modelBuilder.Entity<SeguimientoEnfermedadTrabajo>()
                .HasOne(se => se.EnfermedadRelacionadaTrabajo)
                .WithMany(ec => ec.SeguimientoMedicos)
                .HasForeignKey(se => se.EnfermedadRelacionadaTrabajoId);

            // Configuración de la relación muchos a muchos entre SeguimientoMedico y EnfermedadProfesional
            modelBuilder.Entity<SeguimientoEnfermedadProfesional>()
                .HasKey(se => new { se.SeguimientoMedicoId, se.EnfermedadProfesionalId });

            modelBuilder.Entity<SeguimientoEnfermedadProfesional>()
                .HasOne(se => se.SeguimientoMedico)
                .WithMany(sm => sm.EnfermedadesProfesionales)
                .HasForeignKey(se => se.SeguimientoMedicoId);

            modelBuilder.Entity<SeguimientoEnfermedadProfesional>()
                .HasOne(se => se.EnfermedadProfesional)
                .WithMany(ec => ec.SeguimientoMedicos)
                .HasForeignKey(se => se.EnfermedadProfesionalId);
        }
    }
}