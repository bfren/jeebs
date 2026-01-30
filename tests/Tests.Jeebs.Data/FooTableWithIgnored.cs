// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Data;

public record class FooTableWithIgnored : Table
{
	[Ignore]
	public string Id { get; init; } = "id";

	[Id]
	public string FooId { get; init; } = "foo_id";

	[Ignore]
	public string Bar0 { get; init; } = "foo_bar0";

	public string Bar1 { get; init; } = "foo_bar1";

	public FooTableWithIgnored() : base(Rnd.Str) { }
}
