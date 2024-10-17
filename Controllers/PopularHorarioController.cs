using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewAcupuntura.Commands;

namespace NewAcupuntura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopularHorarioController : ControllerBase
    {
        private readonly PopularHorario _horarioService;

        public PopularHorarioController(PopularHorario horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpPost("popular-horarios")]
        public async Task<IActionResult> PopularHorarios()
        {
            int numeroDeDias = 15;         // Exemplo: 15 dias
            int intervaloDeTempo = 15;     // Exemplo: 15 minutos de intervalo entre horários

            await _horarioService.PopularHorariosAsync(numeroDeDias, intervaloDeTempo);

            return Ok("Horários foram adicionados com sucesso.");
        }
    }
}