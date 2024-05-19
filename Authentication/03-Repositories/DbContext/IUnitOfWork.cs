using System;
using System.Data;

namespace Authentication.Repositories.DbContext
{
	public interface IUnitOfWork
	{
		public IDbConnection Connection { get;}
	}
}

