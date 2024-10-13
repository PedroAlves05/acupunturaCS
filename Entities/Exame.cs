using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewAcupuntura.Entities
{
    public class Exame
    {
        [Key]
        public int Id { get; set; } // Necessário para ser a chave primária no Entity Framework

        [Required]
        public string? Nome { get; set; }

        [Required]
        public float Preco { get; set; }

        [Required]
        public TimeSpan Duracao { get; set; }

        [Required]
        public bool Disponivel { get; set; } = true;
    }
}