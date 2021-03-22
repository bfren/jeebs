// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

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
