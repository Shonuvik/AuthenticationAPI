using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Helpers;
using Authentication.Models;
using Authentication.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services.Interfaces
{
    public class JWTManagerService : IJWTManagerService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public JWTManagerService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<TokenModel> AuthAsync(CredentialModel credentialModel)
        {
            if (credentialModel == null)
                return null;

            var credential = await _userRepository.GetCredentialByUser(credentialModel.UserName)
                ?? throw new ExceptionBusiness("Nao existe credenciais cadastradas com o user informado");

            if (!(credential.ClientId == credentialModel.ClientId && credential.Secret == credentialModel.Secret))
                return null;

            var jwtKey = _configuration["JWTKey"];
            var tokenKey = Encoding.UTF8.GetBytes(jwtKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, credentialModel.ClientId) }),
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
