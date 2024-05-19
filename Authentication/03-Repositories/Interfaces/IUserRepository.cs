using Authentication.Models;

namespace Authentication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task NewUser(CreateCredentialModel model);

        Task<CredentialModel> GetCredentialByUser(string userName);
    }
}

