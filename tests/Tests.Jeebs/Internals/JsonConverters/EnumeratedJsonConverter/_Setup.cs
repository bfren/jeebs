// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;

namespace Jeebs.Internals.JsonConverters.EnumeratedJsonConverter_Tests;

public abstract class Setup
{
	public JsonSerializerOptions GetOptions()
	{
		var opt = new JsonSerializerOptions();
		opt.Converters.Add(new EnumeratedJsonConverterFactory());

		return opt;
	}
}
