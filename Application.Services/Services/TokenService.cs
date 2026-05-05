﻿using Application.Core.Defults;
using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string CreateToken(Teacher Teacher)
        {
            var authclaims = new List<Claim>
            {
                new Claim("id", Teacher.Id.ToString()),
                new Claim("role", Teacher.RoleId)
            };

            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppDefults.Jwt.Key));

            var token = new JwtSecurityToken
            (
                claims: authclaims,
                expires: DateTime.Now.AddDays(AppDefults.Jwt.DurationInDays),
                signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public Teacher? GetTeacherFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(AppDefults.Jwt.Key);
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                }, out SecurityToken validatedToken);
                var userId = principal.FindFirst("id")?.Value;
                var role = principal.FindFirst("role")?.Value;
                return new Teacher
                {
                    Id = long.Parse(userId),
                    RoleId = role,
                };
            }
            catch
            {
                return null;
            }
        }

        public string CreateToken(long Id)
        {
            var authclaims = new List<Claim>
            {
                new Claim("id", Id.ToString()),
            };

            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppDefults.Jwt.Key));

            var token = new JwtSecurityToken
            (
                claims: authclaims,
                expires: DateTime.Now.AddDays(AppDefults.Jwt.DurationInDays),
                signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public long? GetPerantFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(AppDefults.Jwt.Key);
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                }, out SecurityToken validatedToken);
                var userId = principal.FindFirst("id")?.Value;
                return long.Parse(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}