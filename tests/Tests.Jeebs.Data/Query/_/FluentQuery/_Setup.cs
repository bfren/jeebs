// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Logging;
using NSubstitute.Extensions;
using StrongId;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public abstract class QueryFluent_Tests
{
	public static (FluentQuery<TestEntity, TestId> query, Vars v) Setup()
	{
		var db = Substitute.For<IDb>();

		var client = Substitute.For<IDbClient>();
		client.GetQuery<TestEntity, int>(predicates: default!)
			.ReturnsForAnyArgs((Rnd.Str, new QueryParametersDictionary()));
		db.Client
			.Returns(client);

		var unitOfWork = Substitute.For<IUnitOfWork>();
		var transation = Substitute.For<IDbTransaction>();
		unitOfWork.Transaction
			.Returns(transation);
		db.UnitOfWork
			.Returns(unitOfWork);

		var log = Substitute.For<ILog>();
		var repo = Substitute.For<IRepository<TestEntity, TestId>>();

		var query = new FluentQuery<TestEntity, TestId>(db, log);
		repo.StartFluentQuery()
			.ReturnsForAll(query);

		return (query, new(client, db, log, repo, transation));
	}

	public sealed record class Vars(
		IDbClient Client,
		IDb Db,
		ILog Log,
		IRepository<TestEntity, TestId> Repo,
		IDbTransaction Transaction
	);

	public sealed record class TestId : LongId;

	public readonly record struct TestEntity(TestId Id, string? Foo) : IWithId<TestId>;
}