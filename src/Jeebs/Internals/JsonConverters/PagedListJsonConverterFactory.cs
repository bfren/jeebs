// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter factory for <see cref="PagedList{T}"/> objects.
/// </summary>
internal sealed class PagedListJsonConverterFactory : JsonConverterFactory
{
	/// <summary>
	/// Returns true if <paramref name="typeToConvert"/> is a closed generic <see cref="PagedList{T}"/>.
	/// </summary>
	/// <param name="typeToConvert">Type to convert.</param>
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsGenericType
		&& typeToConvert.GetGenericTypeDefinition() == typeof(PagedList<>);

	/// <summary>
	/// Create a <see cref="PagedListJsonConverter{T}"/> for the closed generic type.
	/// </summary>
	/// <param name="typeToConvert">Closed <see cref="PagedList{T}"/> type.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <exception cref="JsonException"/>
	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var itemType = typeToConvert.GetGenericArguments()[0];
		var converterType = typeof(PagedListJsonConverter<>).MakeGenericType(itemType);
		return Activator.CreateInstance(converterType) switch
		{
			JsonConverter x =>
				x,

			_ =>
				throw new JsonException($"Unable to create {typeof(PagedListJsonConverter<>)} for type {typeToConvert}.")
		};
	}
}
