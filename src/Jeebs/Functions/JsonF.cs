// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using System.Text.Json.Serialization;
using StrongId.Json;

namespace Jeebs.Functions;

/// <summary>
/// JSON functions
/// </summary>
public static partial class JsonF
{
	/// <summary>
	/// Empty JSON
	/// </summary>
	public static string Empty { get; } = "\"\"";

	/// <summary>
	/// Default JsonSerializerOptions
	/// </summary>
	private static JsonSerializerOptions Options { get; }

	/// <summary>
	/// Define default settings
	/// </summary>
	static JsonF()
	{
		Options = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.Never,
			DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			NumberHandling = JsonNumberHandling.AllowReadingFromString
		};

		Options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
		Options.Converters.Add(new StrongIdJsonConverterFactory());
		Options.Converters.Add(new Internals.DateTimeConverter());
		Options.Converters.Add(new Internals.EnumeratedConverterFactory());
		Options.Converters.Add(new Internals.MaybeConverterFactory());
	}

	/// <summary>Messages</summary>
	public static partial class M { }
}
