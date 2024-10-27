using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace NewAcupuntura.Entities
{
    public class Horario
    {
        [Key]
        public int Id { get; set; } // Necessário para ser a chave primária

        [Required]
        public DateTime Data { get; set; } // Usando DateTime para armazenar a data


        [Required]
        public bool Disponivel { get; set; } = true; // Default como True
    }
}