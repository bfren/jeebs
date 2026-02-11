// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace AppConsoleRq;

public sealed record class TestTable : Table
{
	public TestTable() : base("console") { }

	[Id]
	public string Id =>
		"id";

	public string Foo =>
		"foo";

	public string Bar =>
		"bar";

	public string Fred =>
		"freddie";
}
