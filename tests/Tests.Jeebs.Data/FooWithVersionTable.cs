// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Data;

public record class FooWithVersionTable : Table
{
	[Id]
	public string FooId { get; } = "foo_with_version_id";

	public string Bar0 { get; } = "foo_with_version_bar0";

	public string Bar1 { get; } = "foo_with_version_bar1";

	[Version]
	public string Version { get; } = "foo_with_version_version";

	public FooWithVersionTable() : base("foo_with_version") { }
}
