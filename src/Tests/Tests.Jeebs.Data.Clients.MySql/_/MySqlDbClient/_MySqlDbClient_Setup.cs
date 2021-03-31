// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests
{
	public static class MySqlDbClient_Setup
	{
		public static (MySqlDbClient client, ITable table) Get()
		{
			var tableName = F.Rnd.Str;
			var table = Substitute.For<ITable>();
			table.GetName().Returns(tableName);
			var client = new MySqlDbClient();

			return (client, table);
		}
	}
}
