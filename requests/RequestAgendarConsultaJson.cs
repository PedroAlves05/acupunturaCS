using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAcupuntura.requests
{
    public class RequestAgendarConsultaJson
    {

        public int ExameId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }

    }
}