using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NewAcupuntura.Entities;
using NewAcupuntura.requests;

namespace NewAcupuntura.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<bool> Authenticate(string email, string senha)
        {
            var result = await _signInManager.PasswordSignInAsync(email, senha, false, lockoutOnFailure: false);
            
            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(RequestRegistrarUsuario request)
        {
            var ApplicationUser = new ApplicationUser {
                Email = request.Email,
                UserName = request.Email,
                Nome = request.Nome,
                PhoneNumber = request.Telefone
            };
            
            var result = await _userManager.CreateAsync(ApplicationUser, request.Senha);

            if(result.Succeeded){
                await _signInManager.SignInAsync(ApplicationUser, isPersistent: false);
            }

            return result.Succeeded;
        }

        public async Task Logout() 
        {
            await _signInManager.SignOutAsync();
        }
    }
}