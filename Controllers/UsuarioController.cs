using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura.Entities;
using NewAcupuntura.Identity;
using NewAcupuntura.requests;


namespace NewAcupuntura.Controllers
{   
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly AcupunturaDbContext _context;

        public UsuarioController(AcupunturaDbContext context, IAuthenticate authenticate){
            _authenticate = authenticate;
            _context = context;
        }

         // Substitua pelo nome do seu DbContext


        [HttpGet("obterHora")]
        public IActionResult obterHora() {
            var obj = new {
                Data = DateTime.Now.ToLongDateString(),
                Hora = DateTime.Now.ToShortTimeString()
            };
            var usuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Content($"ID: {usuario}, Hora: {obj}");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(RequestLoginUsuario request) 
        {
            var result = await _authenticate.Authenticate(request.Email, request.Senha);

            if(result) {
                return Ok(result);
            }

            else{
                ModelState.AddModelError(string.Empty, "Ocorreu um erro.");
                return NoContent();
            }
        }

        [AllowAnonymous]
        [HttpPost("Registrar")]
        public async Task<IActionResult> Register(RequestRegistrarUsuario request) 
        {
 
            var result = await _authenticate.RegisterUser(request);

            if(result)
            {
                return Ok(result);
            }  
            else
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro.");
                return BadRequest();
            }  
        }

        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();
            return Redirect("http://127.0.0.1:5500/html/Cliente/inicio.html");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsuarios()
        {
            var usuarios = await _context.Users
                .Select(u => new 
                {
                    u.Id,
                    u.Email,
                    u.UserName,
                    u.Nome,
                    u.Ativo
                })
                .ToListAsync();

            return Ok(usuarios);
        }
    }
}