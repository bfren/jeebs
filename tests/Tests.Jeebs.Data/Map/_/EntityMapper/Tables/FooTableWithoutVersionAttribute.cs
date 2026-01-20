// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Data.Map.Mapper.Tables;

public record class FooTableWithoutVersionAttribute : Table
{
	[Ignore]
	public string Id { get; init; } = "id";

	[Id]
	public string FooId { get; init; } = "foo_id";

	public string Version { get; init; } = "version";

	public string Bar0 { get; init; } = "bar0";

	public string Bar1 { get; init; } = "bar1";

	public FooTableWithoutVersionAttribute() : base(Rnd.Str) { }
}
