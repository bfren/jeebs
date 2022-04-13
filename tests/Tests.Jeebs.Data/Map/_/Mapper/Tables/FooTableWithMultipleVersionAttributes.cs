// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Data.Map._.Mapper.Tables;

public record class FooTableWithMultipleVersionAttributes : Table
{
	[Ignore]
	public string Id { get; init; } = "id";

	[Id]
	public string FooId { get; init; } = "foo_id";

	[Version]
	public string Version { get; init; } = "version";

	[Version]
	public string Bar0 { get; init; } = "bar0";

	[Version]
	public string Bar1 { get; init; } = "bar1";

	public FooTableWithMultipleVersionAttributes() : base(Rnd.Str) { }
}
