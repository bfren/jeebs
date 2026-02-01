// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Data;

public record class FooUnwriteableTable : Table
{
	[Id]
	public string FooId { get; } = "foo_unwriteable_id";

	[Computed]
	public string Bar2 { get; } = "foo_unwriteable_bar2";

	[Readonly]
	public string Bar3 { get; } = "foo_unwriteable_bar3";

	public FooUnwriteableTable() : base("foo_unwriteable") { }
}
