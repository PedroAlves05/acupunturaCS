using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewAcupuntura.requests
{
    public class RequestRegistrarUsuario
    {
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
    }
}