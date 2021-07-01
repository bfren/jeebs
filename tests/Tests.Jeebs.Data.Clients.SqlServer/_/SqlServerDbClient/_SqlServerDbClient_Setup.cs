// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
