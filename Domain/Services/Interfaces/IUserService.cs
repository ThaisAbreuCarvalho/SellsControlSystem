using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        UserSessionInfo ValidateLogin(string email, string senha);
    }
}
