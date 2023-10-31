using Authentication.Models;

namespace Authentication.Services.Interfaces
{
    public interface IJWTManagerService
	{
		TokenModel AuthAsync(CredentialModel credential);
	}
}

