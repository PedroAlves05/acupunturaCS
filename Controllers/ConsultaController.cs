using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura.Entities;
using NewAcupuntura.requests;

namespace NewAcupuntura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ConsultaController : ControllerBase
    {
        private readonly AcupunturaDbContext _context;

        public ConsultaController(AcupunturaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AgendarConsulta([FromBody] RequestAgendarConsultaJson pedido)
        {
            // Buscar exame e horário pelo ID
            var exame = await _context.Exames.FindAsync(pedido.ExameId);
            if (exame == null)
            {
                return NotFound("Exame não encontrado.");
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var horarioSelecionado = await _context.Horarios
                .FirstOrDefaultAsync(h => h.Data == pedido.Data && h.Hora == pedido.Hora);
            if (horarioSelecionado == null)
            {
                return NotFound("Horário não encontrado.");
            }

            if (!horarioSelecionado.Disponivel)
            {
                return BadRequest("Horário indisponível.");
            }

            // Criar nova consulta
            var consulta = new Consulta
            {
                UsuarioId = userId,
                ExameId = exame.Id,
                HorarioId = horarioSelecionado.Id,
            };

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            // Calcular horário final
            var horarioAgendamento = horarioSelecionado.Hora;
            var duracaoExame = exame.Duracao; // Supondo que 'Duracao' seja TimeSpan
            var horarioFinal = horarioAgendamento + duracaoExame;

            var horarioAtual = horarioAgendamento;

            // Atualizar os horários de 15 em 15 minutos para indisponível
            while (horarioAtual < horarioFinal)
            {
                var horarioParaAtualizar = await _context.Horarios
                    .FirstOrDefaultAsync(h => h.Data == pedido.Data && h.Hora == horarioAtual);

                if (horarioParaAtualizar != null)
                {
                    horarioParaAtualizar.Disponivel = false;
                    _context.Horarios.Update(horarioParaAtualizar);
                }

                // Incrementar horário em 15 minutos
                horarioAtual = horarioAtual.Add(new TimeSpan(0, 15, 0));
            }

            await _context.SaveChangesAsync();

            return Ok("Pedido de exame concluído com sucesso.");
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarConsulta(int id)
        {
            // Busca a consulta pelo Id
            var consulta = await _context.Consultas.FindAsync(id);

            if (consulta == null)
            {
                // Retorna 404 se a consulta não for encontrada
                return NotFound(new { message = "Consulta não encontrada" });
            }

            // Atualiza o campo "Cancelado" para true
            consulta.Cancelado = true;

            // Salva as mudanças no banco de dados
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Retorna erro em caso de falha ao atualizar o banco de dados
                return StatusCode(500, new { message = "Erro ao cancelar a consulta" });
            }

            // Retorna 200 com sucesso
            return Ok(new { message = "Consulta cancelada com sucesso" });
        }

        [HttpGet("usuario/{usuarioId}")]
        public IActionResult PegarConsultasPorUsuario(string usuarioId)
        {
            // Busca todas as consultas associadas ao usuarioId fornecido
            var consultas = _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .Include(c => c.Usuario)
                .Include(c => c.Exame)   // Inclui informações relacionadas ao exame
                .Include(c => c.Horario) // Inclui informações relacionadas ao horário
                .ToList();

            if (consultas == null || consultas.Count == 0)
            {
                return NotFound(new { Message = "Nenhuma consulta encontrada para este usuário." });
            }

            // Mapeamento opcional da resposta (se precisar customizar o retorno)
            var response = consultas.Select(c => new {
                c.Id,
                c.UsuarioId,
                Usuario = new {
                    c.Usuario?.Id,
                    c.Usuario?.Email,
                    c.Usuario?.UserName, // ou qualquer outro campo que você deseje incluir
                    c.Usuario?.Nome // Adicione mais campos conforme necessário
                },
                Exame = new {
                    c.Exame?.Id,
                    c.Exame?.Nome
                },
                Horario = new {
                    c.Horario?.Id,
                    c.Horario?.Data
                },
                c.Cancelado
            });

            return Ok(response);
        }

        [HttpGet("usuario")]
        public IActionResult PegarConsultasPorUsuario()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Busca todas as consultas associadas ao usuarioId fornecido
            var consultas = _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .Include(c => c.Usuario)
                .Include(c => c.Exame)   // Inclui informações relacionadas ao exame
                .Include(c => c.Horario) // Inclui informações relacionadas ao horário
                .ToList();

            if (consultas == null || consultas.Count == 0)
            {
                return NotFound(new { Message = "Nenhuma consulta encontrada para este usuário." });
            }

            // Mapeamento opcional da resposta (se precisar customizar o retorno)
            var response = consultas.Select(c => new {
                c.Id,
                c.UsuarioId,
                Usuario = new {
                    c.Usuario?.Id,
                    c.Usuario?.Email,
                    c.Usuario?.UserName, // ou qualquer outro campo que você deseje incluir
                    c.Usuario?.Nome // Adicione mais campos conforme necessário
                },
                Exame = new {
                    c.Exame?.Id,
                    c.Exame?.Nome
                },
                Horario = new {
                    c.Horario?.Id,
                    c.Horario?.Data
                },
                c.Cancelado
            });

            return Ok(response);
        }

        [HttpGet("consultas-por-dia/{data}")]
        public IActionResult PegarConsultasPorDia(DateTime data)
        {
            // Filtra as consultas pela data específica
            var consultas = _context.Consultas
                .Where(c => c.Horario.Data.Date == data.Date)
                .Include(c => c.Usuario) // Supondo que `Horario.Data` é do tipo DateTime
                .Include(c => c.Exame)   // Inclui informações relacionadas ao exame
                .Include(c => c.Horario) // Inclui informações relacionadas ao horário
                .ToList();

            if (consultas == null || consultas.Count == 0)
            {
                return NotFound(new { Message = "Nenhuma consulta encontrada para o dia especificado." });
            }

            // Mapeamento opcional da resposta (se precisar customizar o retorno)
            var response = consultas.Select(c => new {
                c.Id,
                c.UsuarioId,
                Usuario = new {
                    c.Usuario?.Id,
                    c.Usuario?.Email,
                    c.Usuario?.UserName, // ou qualquer outro campo que você deseje incluir
                    c.Usuario?.Nome // Adicione mais campos conforme necessário
                },
                Exame = new {
                    c.Exame?.Id,
                    c.Exame?.Nome
                },
                Horario = new {
                    c.Horario?.Id,
                    c.Horario?.Data
                },
                c.Cancelado
            });

            return Ok(response);
        }
    }
}