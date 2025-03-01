using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareHaven.Data;
using CareHaven.Models;
using CareHaven.DTOS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace CareHaven.Services
{
    public class AuthService : IAuthService
    {
        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext db, IConfiguration configuration)
        {
            _context = db;
            _configuration = configuration;
        }

        public async Task<(int, string)> Register(UserDTO userDTO, string role)
        {
            try
            {
                if (!IsValidRole(role))
                {
                    return (0, "Invalid role");
                }
                var isExist = _context.Users.FirstOrDefault(u => u.Email == userDTO.Email);
                if (isExist != null)
                {
                    return (2, "User Already Exist!");
                }

                var user = new User
                {
                    Email = userDTO.Email,
                    Username = userDTO.Username,
                    MobileNumber = userDTO.MobileNumber,
                    UserRole = userDTO.UserRole,
                    Password = HashPassword(userDTO.Password)
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return (1, "Registration Successful");
            }
            catch (Exception)
            {
                return (0, "Error Occurs");
            }
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            var _user = _context.Users.FirstOrDefault(e => e.Email == model.Email);
            if (_user == null)
            {
                return (0, "Invalid email");
            }
            if (!VerifyPassword(model.Password, _user.Password))
            {
                return (0, "Invalid password");
            }
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, _user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, _user.Username),
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.Role, _user.UserRole)
                };

            var token = GenerateToken(claims);
            return (1, token);
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsValidRole(string role)
        {
            List<string> validRoles = new List<string> { "Admin", "User" };

            if (!validRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public Task<bool> UserExists(string email)
        {
            throw new NotImplementedException();
        }
    }
}