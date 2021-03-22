using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Auth.Data.Clients.MySql
{
	public sealed class MySqlDbClient : Jeebs.Data.Clients.MySql.MySqlDbClient, IAuthDbClient
	{
		public void MigrateToLatest()
		{
			throw new NotImplementedException();
		}
	}
}
