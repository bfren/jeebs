// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Map;

namespace Jeebs.WordPress.Query.PartsBuilder_Tests;

public abstract class PartsBuilder_Tests
{
	public static (TestPartsBuilder builder, Vars v) Setup()
	{
		var extract = Substitute.For<IExtract>();

		var client = Substitute.For<IDbClient>();

		var schema = Substitute.For<IWpDbSchema>();

		var builder = new TestPartsBuilder(extract, client, schema);

		var table = new TestTable(new DbName(Rnd.Str), Rnd.Str, Rnd.Str);

		return (builder, new(client, schema, table));
	}

	public sealed record class Vars(
		IDbClient Client,
		IWpDbSchema Schema,
		TestTable Table
	);
}

public sealed record class TestId : ULongId<TestId>;

public class TestPartsBuilder : PartsBuilder<TestId>
{
	public TestPartsBuilder(IExtract extract, IDbClient client, IWpDbSchema schema) : base(extract, client, schema)
	{
		Table = Substitute.For<ITable>();
		IdColumn = Substitute.For<IColumn>();
	}

	public override ITable Table { get; }

	public override IColumn IdColumn { get; }
}

public sealed record class TestTable : ITable
{
	private readonly IDbName name;

	public string Id { get; init; }

	public string Foo { get; init; }

	public TestTable(IDbName name, string id, string foo) =>
		(this.name, Id, Foo) = (name, id, foo);

	public IDbName GetName() =>
		name;
}
