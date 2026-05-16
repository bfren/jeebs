// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;

namespace Jeebs.Internals.JsonConverters.PagedListJsonConverter_Tests;

public abstract class Setup
{
	public JsonSerializerOptions GetOptions()
	{
		var opt = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		opt.Converters.Add(new ImmutableListJsonConverterFactory());
		opt.Converters.Add(new PagedListJsonConverterFactory());

		return opt;
	}
}
