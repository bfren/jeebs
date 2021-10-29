// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;

namespace AppConsolePg;

public record JsonTable : Table
{
	public JsonTable(string schema) : base(schema + ".test_json") { }

	public string Id =>
		nameof(Id);

	public string Value =>
		nameof(Value);
}
