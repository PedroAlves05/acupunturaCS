using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewAcupuntura.Entities;
using NewAcupuntura.requests;
using NewAcupuntura.responses;

namespace NewAcupuntura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExameController : ControllerBase
    {
        private readonly AcupunturaDbContext _context;

        public ExameController(AcupunturaDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult AdicionarExame(RequestAdicionarExameJson request)
        {
            var entity = new Exame
            {
                Nome = request.Nome,
                Preco = request.Preco,
                Duracao = request.Duracao
            };

            _context.Exames.Add(entity);
            _context.SaveChanges();

            return Created(string.Empty, entity.Id);
        }


        [HttpGet]
        public IActionResult PegarExames()
        {
            var exames = _context.Exames.Where(exame => exame.Disponivel).ToList();

            var response = new responseExamesJson {
                Exames = exames.Select(exame => new responseExameJson{
                    Id = exame.Id,
                    Nome = exame.Nome,
                    Preco = exame.Preco,
                    Duracao = exame.Duracao
                }).ToList()
            };

            return Ok(response);
        }
    }
}