using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewAcupuntura.requests
{
    public class RequestLoginUsuario
    {
        [Required]
        [EmailAddress] // Valida que o campo contém um email válido
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)] // Indica que o campo é uma senha
        public string? Senha { get; set; }
    }
}