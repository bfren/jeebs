// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Map;
using Jeebs.Logging;
using MaybeF;
using StrongId;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public abstract class FluentQuery_Tests
{
	public static (FluentQuery<TestEntity, TestId> query, Vars v) Setup() =>
		Setup(null, null);

	public static (FluentQuery<TestEntity, TestId> query, Vars v) Setup(string? query, IQueryParametersDictionary? param)
	{
		var client = Substitute.For<IDbClient>();
		client.GetQuery(default!)
			.ReturnsForAnyArgs((query ?? Rnd.Str, param ?? new QueryParametersDictionary()));

		var transation = Substitute.For<IDbTransaction>();

		var unitOfWork = Substitute.For<IUnitOfWork>();
		unitOfWork.Transaction
			.Returns(transation);

		var db = Substitute.For<IDb>();
		db.Client
			.Returns(client);
		db.UnitOfWork
			.Returns(unitOfWork);

		var log = Substitute.For<ILog>();

		var table = new TestTable();

		var map = Substitute.For<ITableMap>();
		map.Table
			.Returns(table);

		var mapper = Substitute.For<IEntityMapper>();
		mapper.GetTableMapFor<TestEntity>()
			.Returns(F.Some(map));

		return (new(db, mapper, log), new(client, db, log, mapper, table, map, transation));
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

	public sealed record class TestId : LongId;

	public sealed record class TestEntity(TestId Id, string? Foo) : IWithId<TestId>;

	public sealed record class TestTable() : Table(Rnd.Str)
	{
		public string Id =>
			nameof(TestTable) + nameof(Id);

		public string Foo =>
			nameof(TestTable) + nameof(Foo);
	}
}
