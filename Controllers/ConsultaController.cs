using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura.Entities;
using NewAcupuntura.requests; // Corrigido para 'Requests'

namespace NewAcupuntura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly AcupunturaDbContext _context;

        public ConsultaController(AcupunturaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AgendarConsulta(RequestAgendarConsultaJson pedido)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            // Validação e conversão manual da data
            if (!DateTime.TryParseExact(pedido.Data, "dd/MM/yyyyTHH:mm:ss", 
                                        System.Globalization.CultureInfo.InvariantCulture, 
                                        System.Globalization.DateTimeStyles.None, 
                                        out var dataConsulta))
            {
                return BadRequest("Formato de data inválido.");
            }

            // Buscar exame pelo ID
            var exame = await _context.Exames.FindAsync(pedido.ExameId);
            if (exame == null)
            {
                return NotFound("Exame não encontrado.");
            }

            // Buscar horário pela data e hora fornecidas
            var horarioSelecionado = await _context.Horarios
                .FirstOrDefaultAsync(h => h.Data == dataConsulta);
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
            var horarioAgendamento = horarioSelecionado.Data; 
            var duracaoExame = exame.Duracao; 
            var horarioFinal = horarioAgendamento + duracaoExame;

            var horarioAtual = horarioAgendamento;

            // Atualizar os horários de 15 em 15 minutos para indisponível
            while (horarioAtual < horarioFinal)
            {
                var horarioParaAtualizar = await _context.Horarios
                    .FirstOrDefaultAsync(h => h.Data == horarioAtual);

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
                .Where(c => c.UsuarioId == usuarioId && c.Cancelado == false)
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
                    c.Usuario?.UserName,
                    c.Usuario?.Nome,
                    c.Usuario?.PhoneNumber
                },
                Exame = new {
                    c.Exame?.Id,
                    c.Exame?.Nome,
                    c.Exame?.Preco
                },
                Horario = new {
                    c.Horario?.Id,
                    c.Horario?.Data // Aqui, o horário é representado apenas pela Data
                },
                c.Cancelado
            });

            return Ok(response);
        }
        

        [HttpGet("pendentes")]
        public IActionResult PegarConsultasPendentesPorUsuario()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized(new { Message = "Usuário não autenticado." });
            }

            // Busca todas as consultas associadas ao usuarioId fornecido
            var consultas = _context.Consultas
                .Where(c => c.UsuarioId == usuarioId && c.Horario.Data > DateTime.Now && c.Cancelado == false)
                .Include(c => c.Usuario)
                .Include(c => c.Exame)
                .Include(c => c.Horario)
                .ToList();

            if (consultas == null || consultas.Count == 0)
            {
                return NotFound("Nenhuma consulta encontrada para este usuário.");
            }

            // Mapeamento opcional da resposta
            var response = consultas.Select(c => new {
                c.Id,
                c.UsuarioId,
                Usuario = new {
                    c.Usuario?.Id,
                    c.Usuario?.Email,
                    c.Usuario?.UserName,
                    c.Usuario?.Nome
                },
                Exame = new {
                    c.Exame?.Id,
                    c.Exame?.Nome,
                    c.Exame?.Preco
                },
                Horario = new {
                    c.Horario?.Id,
                    c.Horario?.Data // Aqui, o horário é representado apenas pela Data
                },
                c.Cancelado
            });

            return Ok(response);
        }

        [HttpGet("finalizadas")]
        public IActionResult PegarConsultaFinalizadasPorUsuario()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized(new { Message = "Usuário não autenticado." });
            }
            // Busca todas as consultas associadas ao usuarioId fornecido
            var consultas = _context.Consultas
                .Where(c => c.UsuarioId == usuarioId && c.Horario.Data < DateTime.Now && c.Cancelado == false)
                .Include(c => c.Usuario)
                .Include(c => c.Exame)
                .Include(c => c.Horario)
                .ToList();

            if (consultas == null || consultas.Count == 0)
            {
                return NotFound("Nenhuma consulta encontrada para este usuário.");
            }

            // Mapeamento opcional da resposta
            var response = consultas.Select(c => new {
                c.Id,
                c.UsuarioId,
                Usuario = new {
                    c.Usuario?.Id,
                    c.Usuario?.Email,
                    c.Usuario?.UserName,
                    c.Usuario?.Nome
                },
                Exame = new {
                    c.Exame?.Id,
                    c.Exame?.Nome,
                    c.Exame?.Preco
                },
                Horario = new {
                    c.Horario?.Id,
                    c.Horario?.Data // Aqui, o horário é representado apenas pela Data
                },
                c.Cancelado
            });

            return Ok(response);
        }

        [HttpGet("consultas-por-dia/{data}")]
        public IActionResult PegarConsultasPorDia(DateTime data)
        {
            // Filtra as consultas pela data específica e que não estão canceladas
            var consultas = _context.Consultas
                .Where(c => c.Horario.Data.Date == data.Date && c.Cancelado == false)
                .Include(c => c.Usuario)
                .Include(c => c.Exame)
                .Include(c => c.Horario)
                .ToList();

            // Filtra as consultas que têm atendimentos cadastrados
            var consultasFiltradas = consultas
                .Where(c => !_context.Atendimentos.Any(a => a.ConsultaId == c.Id))
                .OrderBy(c => c.Horario.Data)
                .ToList();

            if (consultasFiltradas == null || consultasFiltradas.Count == 0)
            {
                return NotFound(new { Message = "Nenhuma consulta encontrada para o dia especificado." });
            }

            // Mapeamento opcional da resposta
            var response = consultasFiltradas.Select(c => new {
                c.Id,
                c.UsuarioId,
                Usuario = new {
                    c.Usuario?.Id,
                    c.Usuario?.Email,
                    c.Usuario?.UserName,
                    c.Usuario?.Nome
                },
                Exame = new {
                    c.Exame?.Id,
                    c.Exame?.Nome
                },
                Horario = new {
                    c.Horario?.Id,
                    c.Horario?.Data // Aqui, o horário é representado apenas pela Data
                },
                c.Cancelado
            });

            return Ok(response);
        }


        [HttpGet("todas-consultas")]
        public IActionResult PegarTodasConsultas()
        {
            // Busca todas as consultas que não estão canceladas
            var consultas = _context.Consultas
                .Where(c => c.Cancelado == false)
                .Include(c => c.Usuario)
                .Include(c => c.Exame)
                .Include(c => c.Horario)
                .ToList();

            // Filtra as consultas que têm atendimentos cadastrados
            var consultasFiltradas = consultas
                .Where(c => !_context.Atendimentos.Any(a => a.ConsultaId == c.Id))
                .OrderBy(c => c.Horario.Data)
                .ToList();

            if (consultasFiltradas == null || consultasFiltradas.Count == 0)
            {
                return NotFound(new { Message = "Nenhuma consulta encontrada." });
            }

            // Mapeamento opcional da resposta
            var response = consultasFiltradas.Select(c => new {
                c.Id,
                c.UsuarioId,
                Usuario = new {
                    c.Usuario?.Id,
                    c.Usuario?.Email,
                    c.Usuario?.UserName,
                    c.Usuario?.Nome
                },
                Exame = new {
                    c.Exame?.Id,
                    c.Exame?.Nome
                },
                Horario = new {
                    c.Horario?.Id,
                    c.Horario?.Data // Representado pela Data
                },
                c.Cancelado
            });

            return Ok(response);
        }


    }
}
