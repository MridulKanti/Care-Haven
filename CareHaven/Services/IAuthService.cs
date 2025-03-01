//using CareHaven.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;


//namespace CareHaven.Services
//{
//    public interface IAuthService
//    {
//        Task<(int, string)> Register(User model, string role);
//        Task<(int, string)> Login(LoginModel model);
//    }
//}

using CareHaven.DTOS;
using CareHaven.Models;
using System.Threading.Tasks;

namespace CareHaven.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Register(UserDTO userDTO, string role);
        Task<(int, string)> Login(LoginModel model);
        Task<bool> UserExists(string email);
    }
}
