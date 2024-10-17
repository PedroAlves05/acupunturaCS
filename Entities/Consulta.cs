using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewAcupuntura.Entities
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; } // Chave primária

        // Relacionamento com a tabela de Usuários
        [ForeignKey("ApplicationUser")]
        public string? UsuarioId { get; set; }
        public ApplicationUser? Usuario { get; set; } // Assumindo que a model de usuário já está definida

        // Relacionamento com a tabela de Exames
        [ForeignKey("Exame")]
        public int ExameId { get; set; }
        public Exame? Exame { get; set; } // Assumindo que a model de exames já está definida

        // Relacionamento com a tabela de Horários
        [ForeignKey("Horario")]
        public int HorarioId { get; set; }
        public Horario? Horario { get; set; } // Assumindo que a model de horários já está definida

        public bool Cancelado { get; set; } = false; // Por padrão, não cancelado
    }
}