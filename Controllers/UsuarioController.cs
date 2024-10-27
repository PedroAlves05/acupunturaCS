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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;


namespace NewAcupuntura.Controllers
{   
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


        [HttpPost("Login")]
        public async Task<IActionResult> Login(RequestLoginUsuario request) 
        {
            var result = await _authenticate.Authenticate(request.Email, request.Senha);

            if (result) 
            {
                // Gere um token JWT
                var usuario = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
                if (usuario == null)
                    return Unauthorized(new { Success = false, Message = "Email ou senha inválidos" });

                var token = GerarToken(usuario);
                return Ok(new { Success = true, Token = token, Message = "Login realizado com sucesso" });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro.");
                return Unauthorized(new { Success = false, Message = "Email ou senha inválidos" });
            }
        }

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
                    u.PhoneNumber,
                    u.Ativo
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> BuscarUsuarios(string nome = null, string telefone = null)
        {
            // Inicializa a consulta de usuários
            var usuariosQuery = _context.Users.AsQueryable();

            // Filtra usuários pelo nome, se fornecido
            if (!string.IsNullOrEmpty(nome))
            {
                usuariosQuery = usuariosQuery.Where(u => u.Nome.Contains(nome));
            }

            // Filtra usuários pelo telefone, se fornecido
            if (!string.IsNullOrEmpty(telefone))
            {
                usuariosQuery = usuariosQuery.Where(u => u.PhoneNumber.Contains(telefone));
            }

            // Executa a consulta e projeta os dados
            var usuarios = await usuariosQuery
                .Select(u => new 
                {
                    u.Id,
                    u.Email,
                    u.UserName,
                    u.Nome,
                    u.PhoneNumber,
                    u.Ativo
                })
                .ToListAsync();

            // Retorna resultado
            if (usuarios.Count == 0)
            {
                return NotFound(new { Message = "Nenhum usuário encontrado com os critérios fornecidos." });
            }

            return Ok(usuarios);
        }


        private string GerarToken(ApplicationUser usuario) 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jj83nHd9H!fmLk$7kPzQx92PfT&NwCqLmT@8Vu#sJfXr0KsPvLzBnQs!")); // Use a chave que você definiu no appsettings.json
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Email, usuario.Email),
                // Adicione outros claims se necessário
            };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5091/",
                audience: "http://127.0.0.1:5500/",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}