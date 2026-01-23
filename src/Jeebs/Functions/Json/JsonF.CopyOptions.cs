// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;

namespace Jeebs.Functions;

public static partial class JsonF
{
	/// <summary>
	/// Get a copy of the default JSON serialiser options.
	/// </summary>
	/// <returns>Default JSON seraliser options.</returns>
	public static JsonSerializerOptions CopyOptions()
	{
		var copy = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = Options.DefaultIgnoreCondition,
			DictionaryKeyPolicy = Options.DictionaryKeyPolicy,
			PropertyNamingPolicy = Options.PropertyNamingPolicy,
			NumberHandling = Options.NumberHandling
		};

		foreach (var item in Options.Converters)
		{
			copy.Converters.Add(item);
		}

		return copy;
	}
}
