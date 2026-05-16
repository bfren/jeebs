// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter factory for <see cref="ImmutableList{T}"/> objects.
/// </summary>
internal sealed class ImmutableListJsonConverterFactory : JsonConverterFactory
{
	/// <summary>
	/// Returns true if <paramref name="typeToConvert"/> is exactly a closed generic <see cref="ImmutableList{T}"/>.
	/// </summary>
	/// <remarks>
	/// Exact match only — subclasses such as <see cref="PagedList{T}"/> are handled by their own factory.
	/// </remarks>
	/// <param name="typeToConvert">Type to convert.</param>
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsGenericType
		&& typeToConvert.GetGenericTypeDefinition() == typeof(ImmutableList<>);

	/// <summary>
	/// Create an <see cref="ImmutableListJsonConverter{T}"/> for the closed generic type.
	/// </summary>
	/// <param name="typeToConvert">Closed <see cref="ImmutableList{T}"/> type.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <exception cref="JsonException"/>
	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var itemType = typeToConvert.GetGenericArguments()[0];
		var converterType = typeof(ImmutableListJsonConverter<>).MakeGenericType(itemType);
		return Activator.CreateInstance(converterType) switch
		{
			JsonConverter x =>
				x,

			_ =>
				throw new JsonException($"Unable to create {typeof(ImmutableListJsonConverter<>)} for type {typeToConvert}.")
		};
	}
}
