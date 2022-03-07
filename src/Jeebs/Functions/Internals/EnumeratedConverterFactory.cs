// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace F.Internals;

/// <summary>
/// Enumerated Converter Factory
/// </summary>
public sealed class EnumeratedConverterFactory : JsonConverterFactory
{
	/// <summary>
	/// Returns true if <paramref name="typeToConvert"/> inherits from <see cref="Jeebs.Enumerated"/>
	/// </summary>
	/// <param name="typeToConvert">Type to convert</param>
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsSubclassOf(typeof(Jeebs.Enumerated));

	/// <summary>
	/// Creates JsonConverter using Enum type as generic argument
	/// </summary>
	/// <param name="typeToConvert">Enum type</param>
	/// <param name="options">JsonSerializerOptions</param>
	/// <exception cref="JsonException"></exception>
	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var converterType = typeof(EnumeratedConverter<>).MakeGenericType(typeToConvert);
		return Activator.CreateInstance(converterType) switch
		{
			JsonConverter x =>
				x,

			_ =>
				throw new JsonException($"Unable to create {typeof(EnumeratedConverter<>)} for type {typeToConvert}.")
		};
	}
}
