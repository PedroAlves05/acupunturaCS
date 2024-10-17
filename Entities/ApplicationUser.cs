using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NewAcupuntura.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public string? Nome { get; set; }

        public bool Ativo { get; set; } = true; // Indica se o usuário está ativo
    }
}