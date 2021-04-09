// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Clients.SqlServer.SqlServerDbClient_Tests
{
	public static class SqlServerClient_Setup
	{
		public static (SqlServerDbClient client, ITable table) Get()
		{
			var tableName = F.Rnd.Str;
			var table = Substitute.For<ITable>();
			table.GetName().Returns(tableName);
			var client = new SqlServerDbClient();

			return (client, table);
		}
	}
}
