using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NewAcupuntura.Entities;
using NewAcupuntura.requests;

namespace NewAcupuntura.Identity
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string email, string senha);
        Task<bool> RegisterUser(RequestRegistrarUsuario request);
        Task Logout();
    }
}