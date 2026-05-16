// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter for <see cref="IPagedList{T}"/> objects.
/// </summary>
/// <typeparam name="T">List item type.</typeparam>
public sealed class PagedListJsonConverter<T> : JsonConverter<IPagedList<T>>
{
	/// <summary>
	/// Read a <see cref="PagedList{T}"/> from a JSON object.
	/// </summary>
	/// <param name="reader">Utf8JsonReader.</param>
	/// <param name="typeToConvert">The type to convert to.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <returns>The deserialized <see cref="PagedList{T}"/>.</returns>
	public override IPagedList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var wrapper = JsonSerializer.Deserialize<PagedListJsonWrapper<T>>(ref reader, options);
		return new PagedList<T>(wrapper.Values ?? new PagingValues(), wrapper.Items ?? []);
	}

	/// <summary>
	/// Ensure Paging Values and Items list are serialised separately.
	/// </summary>
	/// <param name="writer">Utf8JsonWriter.</param>
	/// <param name="value">IPagedList.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	public override void Write(Utf8JsonWriter writer, IPagedList<T> value, JsonSerializerOptions options)
	{
		var wrapper = new PagedListJsonWrapper<T>(PagingValues.From(value.Values), value.AsEnumerable());
		JsonSerializer.Serialize(writer, wrapper, options);
	}
}
