using System;
using System.Data;
using System.Data.SqlClient;

namespace Authentication.Repositories.DbContext
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection connection;

        public UnitOfWork(string conn)
        {
            connection = new SqlConnection(conn);
        }

        public IDbConnection Connection
        {
            get
            {
                connection.Open();
                return connection;
            }
        }
    }
}

