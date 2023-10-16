using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;
using ControleFacil.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // No KeySecret do appsettings.json você pode escrever qualquer coisa, nutella, pizza
            byte[] key = Encoding.UTF8.GetBytes(_configuration["KeySecret"]);

            // Esta variável sabe encriptografar e decriptografar
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
              }),
                Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["HorasValidadeToken"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            };


            SecurityToken token = tokenHandler.CreateToken(tokenDecriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}