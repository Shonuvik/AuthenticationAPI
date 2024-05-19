using System;
using System.Text;
using Authentication.Models;
using Authentication.Repositories.DbContext;
using Authentication.Repositories.Interfaces;
using Dapper;

namespace Authentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _uow;

        public UserRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task NewUser(CreateCredentialModel model)
        {
            StringBuilder query = new();

            query.Append($" INSERT INTO [User]    ");
            query.Append($"            (Name,     ");
            query.Append($"            ClientId,  ");
            query.Append($"            Secret,    ");
            query.Append($"            CreatedAt) ");
            query.Append($"        VALUES         ");
            query.Append($"        (              ");
            query.Append($"            @Name,    ");
            query.Append($"            @ClientId,   ");
            query.Append($"            @Secret,   ");
            query.Append($"            @CreatedAt ");
            query.Append($"        );             ");

            DynamicParameters parameters = new();

            parameters.Add("@Name", model.UserName);
            parameters.Add("@ClientId", model.ClientId);
            parameters.Add("@Secret", model.Secret);
            parameters.Add("@CreatedAt", model.CreatedAt);

            using var connection = _uow.Connection;
            await connection.ExecuteAsync(query.ToString(), parameters);
        }

        public async Task<CredentialModel> GetCredentialByUser(string userName)
        {
            StringBuilder query = new();

            query.Append($" SELECT            ");
            query.Append($"        Name,      ");
            query.Append($"        ClientId,  ");
            query.Append($"        Secret     ");
            query.Append($" FROM [User]       ");
            query.Append($" WHERE Name = @Name");

            using var connection = _uow.Connection;
            var credentials = await connection.QueryFirstOrDefaultAsync<CredentialModel>(query.ToString(),
                new
                {
                    Name = userName
                });

            return credentials;
        }
    }
}

