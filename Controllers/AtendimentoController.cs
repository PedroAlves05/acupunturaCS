using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura.Entities;

namespace NewAcupuntura.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        private readonly AcupunturaDbContext _context;

        public AtendimentoController(AcupunturaDbContext context)
        {
            _context = context;
        }

         [HttpPost]
        public async Task<ActionResult<Atendimento>> PostAtendimento(Atendimento atendimento)
        {
            if (atendimento == null)
            {
                return BadRequest("Dados de atendimento inválidos.");
            }

            // Verifica se a consulta referenciada existe no banco de dados
            var consulta = await _context.Consultas.FindAsync(atendimento.ConsultaId);
            if (consulta == null)
            {
                return NotFound("Consulta não encontrada.");
            }

            // Adiciona o atendimento ao contexto
            _context.Atendimentos.Add(atendimento);

            // Salva as mudanças no banco de dados
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Erro ao salvar os dados de atendimento.");
            }

            // Retorna o atendimento criado e o status 201 Created
            return CreatedAtAction(nameof(GetAtendimento), new { id = atendimento.Id }, atendimento);
        }

        // GET: api/Atendimento/5 (para retornar o atendimento criado pelo método POST)
        [HttpGet("{id}")]
        public async Task<ActionResult<Atendimento>> GetAtendimento(int id)
        {
            var atendimento = await _context.Atendimentos.FindAsync(id);

            if (atendimento == null)
            {
                return NotFound("Atendimento não encontrado.");
            }

            return atendimento;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtendimento(int id)
        {
            var atendimento = await _context.Atendimentos.FindAsync(id);
            if (atendimento == null)
            {
                return NotFound("Atendimento não encontrado.");
            }

            _context.Atendimentos.Remove(atendimento);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Erro ao deletar o atendimento.");
            }

            return NoContent(); // Retorna um 204 No Content após a exclusão bem-sucedida
        }

        [HttpGet("Atendimentos/{consultaId}")]
        public async Task<ActionResult<AtendimentoDetailsDTO>> GetAtendimentosByConsulta(int consultaId)
        {
            // Primeiro, traga os dados necessários do banco
            var atendimento = await _context.Atendimentos
                .Include(a => a.Consulta) // Inclui a consulta associada
                .ThenInclude(c => c.Usuario) // Inclui o usuário associado à consulta
                .FirstOrDefaultAsync(a => a.ConsultaId == consultaId);

            if (atendimento == null)
            {
                return NotFound("Atendimento não encontrado.");
            }

            // Agora, trate os fatores pessoais em memória (após os dados já estarem na memória)
            var fatoresPessoais = new List<string>
            {
                atendimento.Filhos ? "Tem Filhos" : null,
                atendimento.Fumante ? "Fumante" : null,
                atendimento.Alcool ? "Álcool" : null,
                atendimento.Vegetariano ? "Vegetariano" : null,
                atendimento.Drogas ? "Drogas" : null,
                atendimento.AtividadeFisica ? "Atividade Física" : null,
                atendimento.Meditacao ? "Meditação" : null,
                atendimento.PressaoAlta ? "Pressão Alta" : null,
                atendimento.PressaoBaixa ? "Pressão Baixa" : null
            }.Where(f => f != null).ToList(); // Esta lógica é avaliada em memória, evitando o erro

            // Obtenha os pontos tratados
            var pontosTratados = GetPontosTratados(atendimento);

            // Construa o DTO para retornar as informações formatadas
            var atendimentoDetails = new AtendimentoDetailsDTO
            {
                Data = atendimento.data,
                NomePaciente = atendimento.Consulta.Usuario.Nome,
                Peso = atendimento.Peso,
                Altura = atendimento.Altura,
                FatoresPessoais = fatoresPessoais,
                Queixa = atendimento.Queixa,
                Doenca = atendimento.Doenca,
                Tratamento = atendimento.TipoDeTratamento,
                QuantidadeSessoes = atendimento.QuantidadeSessoes,
                Pagamento = atendimento.MetodoDePagamento,
                ConcordouComTermos = atendimento.Concordo,
                PontosTratados = pontosTratados,
                Observacoes = atendimento.Observacao
            };

            return Ok(atendimentoDetails);
        }

        [HttpGet("Atendimentos-financeiro")]
        public async Task<IActionResult> GetAtendimentosFinanceiro()
        {
            // Obtém todos os atendimentos com as consultas e exames relacionados
            var atendimentos = await _context.Atendimentos
                .Include(a => a.Consulta)
                .ThenInclude(c => c.Exame)
                .Include(a => a.Consulta)
                .ThenInclude(c => c.Usuario)
                .ToListAsync();

            if (atendimentos == null || atendimentos.Count == 0)
            {
                return NotFound("Nenhum atendimento encontrado.");
            }

            // Calcula o valor total somando o preço de todos os exames relacionados
            var valorTotal = atendimentos.Sum(a => a.Consulta.Exame.Preco);


            // Monta a resposta com as informações relevantes
            var resultado = atendimentos.Select(a => new
            {
                ConsultaId = a.Consulta.Id,
                Data = a.data.ToString("dd/MM/yy"),
                NomePaciente = a.Consulta.Usuario != null ? a.Consulta.Usuario.Nome : "Paciente Desconhecido",
                Tratamento = a.TipoDeTratamento,
                Valor = a.Consulta.Exame.Preco.ToString("C2") // Formato de moeda
            }).ToList();

            
            return Ok(new
            {
                ValorTotal = valorTotal.ToString("C2"),
                Atendimentos = resultado
            });
        }


        private List<string> GetPontosTratados(Atendimento atendimento)
        {
            var pontosTratados = new List<string>();

            if (atendimento.Olho) pontosTratados.Add("Olho");
            if (atendimento.Olfato) pontosTratados.Add("Olfato");
            if (atendimento.Mandibular) pontosTratados.Add("Mandibular");
            if (atendimento.Pulmoes) pontosTratados.Add("Pulmões");
            if (atendimento.Auditivo) pontosTratados.Add("Auditivo");
            if (atendimento.Estomago) pontosTratados.Add("Estômago");
            if (atendimento.Garganta) pontosTratados.Add("Garganta");
            if (atendimento.Gonadas) pontosTratados.Add("Gônadas");
            if (atendimento.Pancreas) pontosTratados.Add("Pâncreas");
            if (atendimento.Coracao) pontosTratados.Add("Coração");
            if (atendimento.Figado) pontosTratados.Add("Fígado");
            if (atendimento.Retal) pontosTratados.Add("Retal");
            if (atendimento.Ciatico) pontosTratados.Add("Ciático");
            if (atendimento.Joelho) pontosTratados.Add("Joelho");
            if (atendimento.Rim) pontosTratados.Add("Rim");
            if (atendimento.Trigemios) pontosTratados.Add("Trigêmeos");
            if (atendimento.Agressividade) pontosTratados.Add("Agressividade");
            if (atendimento.Tragus) pontosTratados.Add("Tragus");
            if (atendimento.Pele) pontosTratados.Add("Pele");
            if (atendimento.Ombro) pontosTratados.Add("Ombro");
            if (atendimento.MembrosInferiores) pontosTratados.Add("Membros Inferiores");
            if (atendimento.MembrosSuperiores) pontosTratados.Add("Membros Superiores");
            if (atendimento.Alergia) pontosTratados.Add("Alergia");
            if (atendimento.Darwin) pontosTratados.Add("Darwin");
            if (atendimento.Sintase) pontosTratados.Add("Síntase");
            if (atendimento.Talamo) pontosTratados.Add("Tálamo");
            if (atendimento.Occipital) pontosTratados.Add("Occipital");
            if (atendimento.Genital) pontosTratados.Add("Genital");
            if (atendimento.Medular) pontosTratados.Add("Medular");

            return pontosTratados;
        }
    }



    public class AtendimentoDetailsDTO
    {
        public DateTime Data { get; set; }
        public string NomePaciente { get; set; } // Nome do paciente (vem da consulta e usuário)
        public double Peso { get; set; } // Peso do paciente
        public double Altura { get; set; } // Altura do paciente
        public List<string> FatoresPessoais { get; set; } // Fatores pessoais e de saúde (fumante, alcool, etc.)
        public string Queixa { get; set; } // Queixa do paciente
        public string Doenca { get; set; } // Doença, se houver
        public string Tratamento { get; set; } // Tipo de tratamento
        public int QuantidadeSessoes { get; set; } // Quantidade de sessões
        public string Pagamento { get; set; } // Método de pagamento
        public bool ConcordouComTermos { get; set; } // Se o paciente concordou com os termos
        public List<string> PontosTratados { get; set; } // Pontos tratados durante o atendimento
        public string Observacoes { get; set; } // Observações feitas pelo profissional
    }
    
}