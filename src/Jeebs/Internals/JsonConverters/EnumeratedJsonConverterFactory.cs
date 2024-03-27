// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter for <see cref="Enumerated"/> objects.
/// </summary>
internal sealed class EnumeratedJsonConverterFactory : JsonConverterFactory
{
	/// <summary>
	/// Returns true if <paramref name="typeToConvert"/> inherits from <see cref="Enumerated"/>.
	/// </summary>
	/// <param name="typeToConvert">Type to convert.</param>
	/// <returns>Whether or not <paramref name="typeToConvert"/> inherits from <see cref="Enumerated"/>.</returns>
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsSubclassOf(typeof(Enumerated));

	/// <summary>
	/// Creates JsonConverter using <see cref="Enumerated"/> type as generic argument.
	/// </summary>
	/// <param name="typeToConvert"><see cref="Enumerated"/> type.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <returns><see cref="EnumeratedJsonConverter{T}"/> object.</returns>
	/// <exception cref="JsonException"/>
	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var converterType = typeof(EnumeratedJsonConverter<>).MakeGenericType(typeToConvert);
		return Activator.CreateInstance(converterType) switch
		{
			JsonConverter x =>
				x,

			_ =>
				throw new JsonException($"Unable to create {typeof(EnumeratedJsonConverter<>)} for type {typeToConvert}.")
		};
	}
}
