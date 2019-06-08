using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtAuthSample.Models
{
    public class MyTokenValidator : ISecurityTokenValidator
    {
        public bool CanValidateToken =>true;

        public int MaximumTokenSizeInBytes { get; set; }


        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;
            if (securityToken!="abcdef")
            {
                return null;
            }
            var identiy = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identiy.AddClaim(new Claim("name","jesse"));
            identiy.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType,"admin"));
            var principal = new ClaimsPrincipal(identiy);
            return principal;
        }
    }
}
