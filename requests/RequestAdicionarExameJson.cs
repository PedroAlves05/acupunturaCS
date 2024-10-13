using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAcupuntura.requests
{
    public class RequestAdicionarExameJson
    {
        public string? Nome { get; set; }
        public float Preco { get; set; }
        public TimeSpan Duracao { get; set; }
    }
}