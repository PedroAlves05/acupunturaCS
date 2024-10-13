using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura.Entities;


namespace NewAcupuntura
{
    public class JourneyDbContext : DbContext
    {
        public DbSet<Exame> Exames { get; set;}
        public DbSet<Horario> Horarios { get; set;}
        public DbSet<Usuario> Usuarios { get; set;}
        public DbSet<Consulta> Consultas { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\pedro\\OneDrive\\Área de Trabalho\\NewAcupuntura\\acupuntura.db");
        }
    }
}