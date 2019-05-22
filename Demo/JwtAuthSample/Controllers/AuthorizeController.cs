using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtAuthSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace JwtAuthSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private JwtSettings _jwtSeetigns;
        public AuthorizeController(IOptions<JwtSettings> _jwtSeetignsAccesser)
        {
            _jwtSeetigns = _jwtSeetignsAccesser.Value;
        }
        [HttpPost]
        public IActionResult Token([FromBody]LoginViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                if (!(viewmodel.User == "jesse" && viewmodel.password == "123456"))
                {
                    return BadRequest();
                }

                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name,"jesse"),
                     new Claim(ClaimTypes.Role,"admin")
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSeetigns.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _jwtSeetigns.Issuser,
                    _jwtSeetigns.Audience,
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(1),
                    creds
                    );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest();
        }
    }
}