using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace NewAcupuntura
{
    public class AcupunturaDbContext : IdentityDbContext<ApplicationUser>
    {
        public AcupunturaDbContext(DbContextOptions<AcupunturaDbContext> options) : base(options)
        {
        }
        public DbSet<Exame> Exames { get; set;}
        public DbSet<Horario> Horarios { get; set;}
        public DbSet<Usuario> Usuarios { get; set;}
        public DbSet<Consulta> Consultas { get; set;}
        public DbSet<Atendimento> Atendimentos { get; set;}
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atendimento>()
                .HasIndex(a => a.ConsultaId)
                .IsUnique(); // Garante que ConsultaId seja único

            base.OnModelCreating(modelBuilder);
        }
    }
}