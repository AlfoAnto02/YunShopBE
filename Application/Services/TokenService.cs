using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Models.Request;
using Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services {
    public class TokenService : ITokenService{
        private readonly JwtAuthenticationOption jwtAuthenticationOption;

        public TokenService(IOptions<JwtAuthenticationOption> jwtAuthenticationOption) {
            this.jwtAuthenticationOption = jwtAuthenticationOption.Value;
        }

        public async Task<string> CreateToken(CreateTokenRequest createTokenRequest) {
            List<Claim> claims = new List<Claim> {
                new Claim("user_id", createTokenRequest.userId),
                new Claim("username", createTokenRequest.userName),
                new Claim("email", createTokenRequest.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthenticationOption.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                issuer: jwtAuthenticationOption.Issuer,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
