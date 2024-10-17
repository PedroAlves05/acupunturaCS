using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewAcupuntura.Entities;

namespace NewAcupuntura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HorarioController : ControllerBase
    {
        private readonly AcupunturaDbContext _context;

        public HorarioController(AcupunturaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostHorario([FromBody] Horario novoHorario)
        {
            if (novoHorario == null)
            {
                return BadRequest(new { Message = "Dados inválidos." });
            }

            var horarioExistente = _context.Horarios
                .Any(h => h.Data == novoHorario.Data && h.Hora == novoHorario.Hora);

            if (horarioExistente)
            {
                return Conflict(new { Message = "Já existe um horário marcado para esta data e hora." });
            }

            _context.Horarios.Add(novoHorario);
            _context.SaveChanges(); 

            return CreatedAtAction(nameof(GetHorarioById), new { id = novoHorario.Id }, novoHorario);
        }

        [HttpGet("{id}")]
        public IActionResult GetHorarioById(int id)
        {
            var horario = _context.Horarios.Find(id);

            if (horario == null)
            {
                return NotFound();
            }

            return Ok(horario);
        }

        [HttpGet("dia/{dia}")]
        public IActionResult PegarHorariosPorDia(DateTime dia)
        {
            var agora = DateTime.Now;

            var horarios = _context.Horarios
                .Where(h => h.Data.Date == dia.Date && h.Disponivel == true && h.Data > agora) // Verifica se o horário está disponível e à frente de DateTime.Now
                .ToList();

            if (horarios == null || horarios.Count == 0)
            {
                return NotFound(new { Message = "Nenhum horário disponível encontrado para este dia." });
            }

            return Ok(horarios);
        }
        
    }
}