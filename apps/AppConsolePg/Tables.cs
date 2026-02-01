// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace AppConsolePg;

public sealed record class TestTable : Table
{
	public TestTable(string schema) : base(schema, "test") { }

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

public sealed record class JsonTable : Table
{
	public JsonTable(string schema) : base(schema, "test_json") { }

	[Id]
	public string Id =>
		"json_id";

	public string Value =>
		"json_value";
}
