using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Models;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services.Interfaces
{
    public class JWTManagerService : IJWTManagerService
    {
        private readonly IConfiguration _configuration;

        public JWTManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenModel AuthAsync(CredentialModel credential)
        {
            var clientId = _configuration["ClientId"];
            var secret = _configuration["Secret"];

            if (credential is null)
                return null;

            if (!(credential.ClientId == clientId && credential.Secret == secret))
                return null;

            var jwtKey = _configuration["JWTKey"];
            var tokenKey = Encoding.UTF8.GetBytes(jwtKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, credential.ClientId) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            TokenModel tokenModel = new TokenModel();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenModel.Token = tokenHandler.WriteToken(token);
            return tokenModel;
        }
    }
}

