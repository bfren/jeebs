// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using Jeebs.Data.Querying;
using NSubstitute;

namespace Jeebs.Data.DbQuery_Tests
{
	public static class DbQuery_Setup
	{
		public static (IDb db, IDbClient client, ILog log, DbQuery<IDb> query) Get(string? queryText = null, IQueryParameters? queryParams = null)
		{
			var text = queryText ?? F.Rnd.Str;
			var param = queryParams ?? Substitute.For<IQueryParameters>();

			var client = Substitute.For<IDbClient>();
			client.GetCountQuery(Arg.Any<IQueryParts>()).Returns((text, param));
			client.GetQuery(Arg.Any<IQueryParts>()).Returns((text, param));

			var db = Substitute.For<IDb>();
			db.Client.Returns(client);
			db.ExecuteAsync(text, param, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(true);
			db.ExecuteAsync<long>(text, param, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(1);

			var log = Substitute.For<ILog>();
			var query = Substitute.ForPartsOf<DbQuery<IDb>>(db, log);

			return (db, client, log, query);
		}

		public static (IQueryParts parts, IQueryParameters param) GetParts()
		{
			var param = Substitute.For<IQueryParameters>();
			var parts = Substitute.For<IQueryParts>();

			return (parts, param);
		}
	}
}
