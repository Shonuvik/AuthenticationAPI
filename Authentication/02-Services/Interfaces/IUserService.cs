using System;
using Authentication.Models;

namespace Authentication.Services.Interfaces
{
	public interface IUserService
	{
        Task NewUser(CredentialModel credential);
    }
}

