﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopDomain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ShopApplication.SideServices
{
    public class JwtProvider(IOptions<JwtOptions> options) 
    {
        private readonly JwtOptions _jwtOptions = options.Value;
        public string GenerateToken(User user)
        {
            Claim[] claims = [new("userId", user.Id.ToString()), new("userRole", user.Role.ToString())];
            var signingCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                algorithm: SecurityAlgorithms.HmacSha256
            );
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
        
    }
}
