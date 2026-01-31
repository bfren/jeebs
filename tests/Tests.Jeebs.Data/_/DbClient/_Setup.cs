// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Data.Attributes;
using Jeebs.Data.Map;
using Jeebs.Logging;

namespace Jeebs.Data.DbClient_Tests;

public abstract class DbClient_Setup
{
	public static (DbClient client, Vars v) Setup()
	{
		var connectionString = Rnd.Str;
		var config = new DbConnectionConfig { ConnectionString = connectionString };

		var log = Substitute.For<ILog>();

		var client = Substitute.ForPartsOf<DbClient>();
		client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).ColName}--");
		client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

		var map = Map<TestEntity>.To<TestTable>();

		var entities = Substitute.For<IEntityMapper>();
		entities.GetTableMapFor<TestEntity>().Returns(map.Wrap().AsResult());

		var db = Substitute.ForPartsOf<Db>(client, config, log);

		return (client, new(entities, log, config, db));
	}

	public sealed record class Vars(
		IEntityMapper Entities,
		ILog Log,
		DbConnectionConfig Config,
		Db db
	);

	public sealed record class TestId : LongId<TestId>;

	public sealed record class TestEntity : WithId<TestId, long>
	{
		public int Foo { get; init; }

		public string? Bar { get; init; }

		public Guid Ignore { get; init; }
	}

	public sealed record class TestModel(int Foo, string? Bar);

	public sealed record class TestTable() : Table(Rnd.Str)
	{
		[Id]
		public string Foo =>
			Rnd.Str;

		public string Bar =>
			Rnd.Str;

		public string Ignore =>
			Rnd.Str;
	}
}
