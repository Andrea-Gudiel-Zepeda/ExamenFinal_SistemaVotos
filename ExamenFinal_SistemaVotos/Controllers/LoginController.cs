using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExamenFinal_SistemaVotos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [Route("Login")]
        [HttpPost]
        public BD_SVotos.Token LoginAPILogin(BD_SVotos.Token tokenRequest)
        {
            BD_SVotos.Token tokenResult = new BD_SVotos.Token();


            if (tokenRequest.token == "asdkhfalskdjfhas")
            {
                string applicationName = "ExamenFinal_SistemaVotos";
                tokenResult.expirationTime = DateTime.Now.AddMinutes(30);
                tokenResult.token = CustomTokenJWT(applicationName, tokenResult.expirationTime);
            }

            return tokenResult;

        }

        private string CustomTokenJWT(string ApplicationName, DateTime token_expiration)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();
            BD_SVotos.JWTResult jWTResult = config.GetRequiredSection("JWT").Get<BD_SVotos.JWTResult>();

            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jWTResult.SecretKey)
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, ApplicationName),
                new Claim("Name", "nombrepersona")
            };
            var _Payload = new JwtPayload(
                    issuer: jWTResult.Issuer,
                    audience: jWTResult.Audience,
                    claims: _Claims,
                    notBefore: DateTime.Now,
                    expires: token_expiration
                );
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }


    }
}
