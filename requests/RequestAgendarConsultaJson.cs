using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAcupuntura.requests
{
    public class RequestAgendarConsultaJson
    {
        public int ExameId { get; set; }
        public string Data { get; set; } // Data como string para converter no controller
    }

}