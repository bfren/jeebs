// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters;

internal sealed class PagedListJsonConverterFactory : JsonConverterFactory
{
	/// <summary>
	/// Returns true if <paramref name="typeToConvert"/> is <see cref="IPagedList{T}"/>
	/// or <see cref="PagedList{T}"/>.
	/// </summary>
	/// <param name="typeToConvert">Type to convert.</param>
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsGenericType
		&& new[] { typeof(IPagedList<>), typeof(PagedList<>) }.Contains(typeToConvert.GetGenericTypeDefinition());

	/// <summary>
	/// Create a <see cref="PagedListJsonConverter{T}"/> for the generic type.
	/// </summary>
	/// <param name="typeToConvert">IPagedList type.</param>
	/// <param name="options">JsonSerializerOptions.</param>
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
