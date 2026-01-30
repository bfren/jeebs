// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.QueryBuilder;
using Jeebs.Logging;

namespace Jeebs.Data.Common.DbQuery_Tests;

public static class DbQuery_Setup
{
	public static (IDb db, IDbClient client, ILog log, DbQuery<IDb> query) Get() =>
		Get(null, null);

	public static (IDb db, IDbClient client, ILog log, DbQuery<IDb> query) Get(string? queryText) =>
		Get(queryText, null);

	public static (IDb db, IDbClient client, ILog log, DbQuery<IDb> query) Get(IQueryParametersDictionary? queryParams) =>
		Get(null, queryParams);

	public static (IDb db, IDbClient client, ILog log, DbQuery<IDb> query) Get(string? queryText, IQueryParametersDictionary? queryParams)
	{
		var text = queryText ?? Rnd.Str;
		var param = queryParams ?? Substitute.For<IQueryParametersDictionary>();

		var client = Substitute.For<IDbClient>();
		client.GetCountQuery(Arg.Any<IQueryParts>()).Returns((text, param));
		client.GetQuery(Arg.Any<IQueryParts>()).Returns((text, param));

		var db = Substitute.For<IDb>();
		var results = Substitute.For<IEnumerable<int>>();
		db.Client.Returns(client);
		db.QueryAsync<int>(text, param, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(R.Wrap(results));
		db.QuerySingleAsync<int>(text, param, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(R.Wrap(Rnd.Int));
		db.ExecuteAsync(text, param, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(R.True);
		db.ExecuteAsync<ulong>(text, param, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(R.Wrap(Rnd.UInt64));

		var log = Substitute.For<ILog>();
		var query = Substitute.ForPartsOf<DbQuery<IDb>>(db, log);

		return (db, client, log, query);
	}

	public static (IQueryParts parts, IQueryParametersDictionary param) GetParts()
	{
		var param = Substitute.For<IQueryParametersDictionary>();
		var parts = Substitute.For<IQueryParts>();

		return (parts, param);
	}
}
