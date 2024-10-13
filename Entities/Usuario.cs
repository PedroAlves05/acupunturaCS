using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewAcupuntura.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; } // Chave primária auto-incrementada

        [Required]
        public string? Nome { get; set; }

        [Required]
        [Phone] // Valida que o campo contém um número de telefone válido
        public string? Telefone { get; set; }

        [Required]
        [EmailAddress] // Valida que o campo contém um email válido
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)] // Indica que o campo é uma senha
        public string? Senha { get; set; }

        [Required]
        public bool Ativo { get; set; } = true; // Indica se o usuário está ativo
    }
}