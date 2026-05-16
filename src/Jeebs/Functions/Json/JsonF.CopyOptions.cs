// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using System.Text.Json.Serialization;

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
			DefaultIgnoreCondition = JsonIgnoreCondition.Never,
			DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
			NumberHandling = JsonNumberHandling.AllowReadingFromString,
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		foreach (var item in Options.Converters)
		{
			copy.Converters.Add(item);
		}

		return copy;
	}
}
