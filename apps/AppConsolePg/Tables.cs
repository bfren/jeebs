// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace AppConsolePg;

public record JsonTable : Table
{
	public JsonTable(string schema) : base(schema, "test_json") { }

	[Id]
	public string Id =>
		nameof(Id);

	public string Value =>
		nameof(Value);
}
