// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Attributes;

namespace AppConsolePg;

public sealed record class JsonTable : Table
{
	public JsonTable(string schema) : base(schema, "test_json") { }

	[Id]
	public string Id =>
		"json_id";

	public string Value =>
		"json_value";
}
