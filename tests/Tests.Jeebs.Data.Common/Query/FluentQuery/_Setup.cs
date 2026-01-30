// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Query;
using Jeebs.Logging;

namespace Jeebs.Data.Common.Query.FluentQuery_Tests;

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

		var transaction = Substitute.For<IDbTransaction>();

		var unitOfWork = Substitute.For<IUnitOfWork>();
		unitOfWork.Transaction.Returns(transaction);

		var db = Substitute.For<IDb>();
		db.Client.Returns(client);
		db.StartWork().Returns(unitOfWork);
		db.StartWorkAsync().Returns(unitOfWork);
		db.QueryAsync<long>(notNullQuery, notNullParam, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(new[] { Rnd.Lng });
		db.QueryAsync<string>(notNullQuery, notNullParam, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(new[] { Rnd.Str });
		db.QuerySingleAsync<long>(notNullQuery, notNullParam, CommandType.Text, Arg.Any<IDbTransaction>()).Returns(Rnd.Lng);

		var log = Substitute.For<ILog>();

		var table = new TestTable();

		var map = Substitute.For<ITableMap>();
		map.Table.Returns(table);

		var mapper = Substitute.For<IEntityMapper>();
		mapper.GetTableMapFor<TestEntity>().Returns(R.Wrap(map));

		return (new(db, mapper, log), new(client, db, log, mapper, table, map, transaction));
	}

	public sealed record class Vars(
		IDbClient Client,
		IDb Db,
		ILog Log,
		IEntityMapper Mapper,
		TestTable Table,
		ITableMap TableMap,
		IDbTransaction Transaction
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
