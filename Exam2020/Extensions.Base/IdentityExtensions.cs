using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Extensions.Base
{
    public static class IdentityExtensions
    {

        public static Guid? GetUserId(this ClaimsPrincipal user)
        {

            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                return Guid.Parse(userId);
            }


            return null;
        }
        public static string GetUserEmail(this ClaimsPrincipal user)
        {

            var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email != null)
            {
                return email;
            }


            return "null";
        }

        public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience,
            DateTime expirationDateTime)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expirationDateTime,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}