using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Cryptograph _cryptograph;

        public UserService(IUserRepository userRepository, Cryptograph cryptograph)
        {
            _userRepository = userRepository;
            _cryptograph = cryptograph;
        }

        public UserSessionInfo ValidateLogin(string email, string senha)
        {
            var senhaRequest = _cryptograph.GetMD5Hash(senha);

            var user = _userRepository.Select(new Usuario { }).Find(x => x.Email == email && x.Senha == senhaRequest);

            return new UserSessionInfo
            {
                Code = user?.Codigo.ToString(),
                Email = user?.Email,
                Error = user == null ? "Usuário não encontrado" : string.Empty,
                IsValid = user != null,
                Name = user?.Nome
            };
        }
    }
}
