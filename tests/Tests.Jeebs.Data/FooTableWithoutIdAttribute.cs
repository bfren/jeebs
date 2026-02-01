// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Data;

public record class FooTableWithoutIdAttribute : Table
{
	[Ignore]
	public string Id { get; init; } = "id";

	public string FooId { get; init; } = "foo_id";

	public string Bar0 { get; init; } = "bar0";

	public string Bar1 { get; init; } = "bar1";

	public FooTableWithoutIdAttribute() : base(Rnd.Str) { }
}
