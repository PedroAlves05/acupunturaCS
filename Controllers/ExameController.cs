using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewAcupuntura.Entities;
using NewAcupuntura.requests;

namespace NewAcupuntura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExameController : ControllerBase
    {
        [HttpPost]
        public IActionResult AdicionarExame(RequestAdicionarExameJson request)
        {
            var dbContext = new JourneyDbContext();
            var entity = new Exame
            {
                Nome = request.Nome,
                Preco = request.Preco,
                Duracao = request.Duracao
            };

            dbContext.Exames.Add(entity);
            dbContext.SaveChanges();

            return Created(string.Empty, entity.Id);
        }
    }
}