// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Data.Common.Repository_Tests;

public abstract class Repository_Setup
{
	public (Repository<Foo, FooId> repo, Vars v) Setup()
	{
		var client = Substitute.For<IDbClient>();
		client.GetCreateQuery<Foo>().Returns(Rnd.Str);
		client.GetRetrieveQuery<Foo, FooModel>().Returns(Rnd.Str);
		client.GetUpdateQuery<Foo, FooModel>().Returns(Rnd.Str);
		client.GetDeleteQuery<Foo>().Returns(Rnd.Str);

		var unitOfWork = Substitute.For<IUnitOfWork>();
		unitOfWork.CommitAsync(Arg.Any<FooId>()).Returns(x => R.Wrap(x.Arg<FooId>()));
		unitOfWork.CommitAsync(Arg.Any<bool>()).Returns(x => R.Wrap(x.Arg<bool>()));

		var db = Substitute.For<IDb>();
		db.Client.Returns(client);
		db.InsertAsync<FooId>(default!, default, default, default!).ReturnsForAnyArgs(IdGen.LongId<FooId>());
		db.ExecuteAsync(default!, default, default, default!).ReturnsForAnyArgs(true);
		db.DoWorkAsync(Arg.Any<Func<IUnitOfWork, Task<Result<FooModel>>>>())
			.Returns(x => x.Arg<Func<IUnitOfWork, Task<Result<FooModel>>>>()(unitOfWork));
		db.DoWorkAsync(Arg.Any<Func<IUnitOfWork, Task<Result<FooId>>>>())
			.Returns(x => x.Arg<Func<IUnitOfWork, Task<Result<FooId>>>>()(unitOfWork).BindAsync(unitOfWork.CommitAsync));
		db.DoWorkAsync(Arg.Any<Func<IUnitOfWork, Task<Result<bool>>>>())
			.Returns(x => x.Arg<Func<IUnitOfWork, Task<Result<bool>>>>()(unitOfWork).BindAsync(unitOfWork.CommitAsync));

		var log = Substitute.For<ILog<Repository<Foo, FooId>>>();

		var repo = Substitute.ForPartsOf<Repository<Foo, FooId>>(db, log);

		return (repo, new(db, client, log, unitOfWork));
	}

	public sealed record class Vars(
		IDb Db,
		IDbClient Client,
		ILog Log,
		IUnitOfWork UnitOfWork
	);

	public sealed record class Foo : WithId<FooId, long>;

	public sealed record class FooModel : WithId<FooId, long>;

	public sealed record class FooId : LongId<FooId>;
}
