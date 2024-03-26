// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Functions.JsonConverters;
using Wrap.Json;

namespace Jeebs.Functions;

/// <summary>
/// JSON functions.
/// </summary>
public static partial class JsonF
{
	/// <summary>
	/// Empty JSON string.
	/// </summary>
	public static string Empty { get; } = "\"\"";

	/// <summary>
	/// Default JsonSerializerOptions.
	/// </summary>
	public static JsonSerializerOptions Options { private get; set; }

	/// <summary>
	/// Define default settings.
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

		Options.Converters.Add(new DateTimeJsonConverter());
		Options.Converters.Add(new EnumeratedJsonConverterFactory());
		Options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
		Options.Converters.Add(new MaybeJsonConverterFactory());
		Options.Converters.Add(new UnionJsonConverterFactory());
	}
}
