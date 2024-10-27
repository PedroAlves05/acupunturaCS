using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAcupuntura.responses
{
    public class responseExameJson
    {
        public int Id { get; set; } // Necessário para ser a chave primária no Entity Framework

        public string? Nome { get; set; }
        
        public string? Descricao { get; set; }

        public float Preco { get; set; }

        public TimeSpan Duracao { get; set; }
    }
}