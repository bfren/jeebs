// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Data.Map.Mapper_Tests;

public record class FooTableWithMultipleIdAttributes : Table
{
	[Ignore]
	public string Id { get; init; } = "id";

	[Id]
	public string FooId { get; init; } = "foo_id";

	[Id]
	public string Bar0 { get; init; } = "bar0";

	[Id]
	public string Bar1 { get; init; } = "bar1";

	public FooTableWithMultipleIdAttributes() : base(Rnd.Str) { }
}
