using System;
using Authentication.Models;
using Authentication.Repositories.Interfaces;
using Authentication.Services.Interfaces;

namespace Authentication.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task NewUser(CredentialModel credential)
        {
            if (credential == null)
                throw new ArgumentNullException(nameof(CredentialModel));

            var model = Map(credential);
            await _userRepository.NewUser(model);
        }

        public CreateCredentialModel Map(CredentialModel credential)
        {
            var model = new CreateCredentialModel();
            model.ClientId = credential.ClientId;
            model.UserName = credential.UserName;
            model.Secret = credential.Secret;
            model.CreatedAt = DateTime.Now;

            return model;
        }
    }
}

