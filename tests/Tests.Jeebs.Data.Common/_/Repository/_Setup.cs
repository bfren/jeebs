// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Logging;

namespace Jeebs.Data.Common.Repository_Tests;

public abstract class Repository_Setup
{
	public (Repository<Foo, FooId> repo, Vars v) Setup()
	{
		var client = Substitute.For<IDbClient>();
		client.GetCreateQuery<Foo>().Returns(Rnd.Str);
		client.GetRetrieveQuery<Foo, FooModel>(Arg.Any<long>()).Returns(Rnd.Str);
		client.GetUpdateQuery<Foo, FooModel>(Arg.Any<long>()).Returns(Rnd.Str);
		client.GetDeleteQuery<Foo>(Arg.Any<long>()).Returns(Rnd.Str);

		var transaction = Substitute.For<IDbTransaction>();

		var w = Substitute.For<IUnitOfWork>();
		w.Transaction.Returns(transaction);
		w.CommitAsync(Arg.Any<FooId>()).Returns(x => R.Wrap(x.Arg<FooId>()));
		w.CommitAsync(Arg.Any<bool>()).Returns(x => R.Wrap(x.Arg<bool>()));

		var db = Substitute.For<IDb>();
		db.Client.Returns(client);
		db.InsertAsync<FooId>(default!, default, default, default!).ReturnsForAnyArgs(IdGen.LongId<FooId>());
		db.ExecuteAsync(default!, default, default, default!).ReturnsForAnyArgs(true);
		db.StartWorkAsync().Returns(w);

		var log = Substitute.For<ILog<Repository<Foo, FooId>>>();

		var repo = Substitute.ForPartsOf<Repository<Foo, FooId>>(db, log);

		return (repo, new(db, client, log, transaction, w));
	}

	public sealed record class Vars(
		IDb Db,
		IDbClient Client,
		ILog Log,
		IDbTransaction Transaction,
		IUnitOfWork UnitOfWork
	);

	public sealed record class Foo : WithId<FooId, long>;

	public sealed record class FooModel : WithId<FooId, long>;

	public sealed record class FooId : LongId<FooId>;
}
