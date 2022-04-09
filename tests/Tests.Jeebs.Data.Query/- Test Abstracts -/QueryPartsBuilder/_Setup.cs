// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using StrongId;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public abstract class QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : class, IStrongId, new()
{
	protected abstract TBuilder GetConfiguredBuilder(IExtract extract);

	public (TBuilder builder, Vars v) Setup()
	{
		var extract = Substitute.For<IExtract>();

		var builder = GetConfiguredBuilder(extract);

		var parts = new QueryParts(builder.Table);

		return (builder, new(extract, parts));
	}

	public record class Vars(
		IExtract Extract,
		QueryParts Parts
	);
}

public record class TestTable0 : ITable
{
	private readonly IDbName name;

	public string Foo { get; init; }

	public TestTable0(IDbName name, string foo) =>
		(this.name, Foo) = (name, foo);

	public IDbName GetName() =>
		name;
}

public record class TestTable1 : ITable
{
	private readonly IDbName name;

	public string Bar { get; init; }

	public TestTable1(IDbName name, string bar) =>
		(this.name, Bar) = (name, bar);

	public IDbName GetName() =>
		name;
}
