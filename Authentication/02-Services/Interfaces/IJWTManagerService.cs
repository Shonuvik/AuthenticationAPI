using Authentication.Models;

namespace Authentication.Services.Interfaces
{
    public interface IJWTManagerService
	{
		Task<TokenModel> AuthAsync(CredentialModel credential);
	}
}

