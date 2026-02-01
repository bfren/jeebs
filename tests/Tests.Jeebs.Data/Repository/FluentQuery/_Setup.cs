// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Logging;

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public abstract class FluentQuery_Tests
{
	public static (FluentQuery<TestEntity, TestId> query, Vars v) Setup() =>
		Setup(null, null);

	public static (FluentQuery<TestEntity, TestId> query, Vars v) Setup(string? query, IQueryParametersDictionary? param)
	{
		var client = Substitute.For<IDbClient>();
		var notNullQuery = query ?? Rnd.Str;
		var notNullParam = param ?? new QueryParametersDictionary();
		client.GetQuery(default!).ReturnsForAnyArgs((notNullQuery, notNullParam));
		client.GetCountQuery(default!).ReturnsForAnyArgs((notNullQuery, notNullParam));

		var db = Substitute.For<IDb>();
		db.Client.Returns(client);
		db.QueryAsync<long>(notNullQuery, notNullParam).Returns(new[] { Rnd.Lng });
		db.QueryAsync<string>(notNullQuery, notNullParam).Returns(new[] { Rnd.Str });
		db.QuerySingleAsync<long>(notNullQuery, notNullParam).Returns(Rnd.Lng);

		var log = Substitute.For<ILog>();

		var table = new TestTable();

		var map = Substitute.For<ITableMap>();
		map.Table.Returns(table);

		var mapper = Substitute.For<IEntityMapper>();
		mapper.GetTableMapFor<TestEntity>().Returns(R.Wrap(map));

		return (new(db, mapper, log), new(client, db, log, mapper, table, map));
	}

	public sealed record class Vars(
		IDbClient Client,
		IDb Db,
		ILog Log,
		IEntityMapper Mapper,
		TestTable Table,
		ITableMap TableMap
	);

	public sealed record class TestId : LongId<TestId>;

	public sealed record class TestEntity(string? Foo) : WithId<TestId, long>;

	public sealed record class TestTable() : Table(Rnd.Str)
	{
		public string Id =>
			nameof(TestTable) + nameof(Id);

		public string Foo =>
			nameof(TestTable) + nameof(Foo);
	}
}
