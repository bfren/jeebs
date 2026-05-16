// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Collections;
using Jeebs.Functions;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter for <see cref="ImmutableList{T}"/> objects.
/// </summary>
/// <typeparam name="T">List item type.</typeparam>
public sealed class ImmutableListJsonConverter<T> : JsonConverter<IImmutableList<T>>
{
	/// <summary>
	/// Read a <see cref="ImmutableList{T}"/> from a JSON object.
	/// </summary>
	/// <param name="reader">Utf8JsonReader.</param>
	/// <param name="typeToConvert">The type to convert to.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <returns>The deserialized <see cref="ImmutableList{T}"/>.</returns>
	public override IImmutableList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var wrapper = JsonSerializer.Deserialize<ImmutableListJsonWrapper<T>>(ref reader, options);
		return ListF.Create(wrapper.Items);
	}

	/// <summary>
	/// Ensure Paging Values and Items list are serialised separately.
	/// </summary>
	/// <param name="writer">Utf8JsonWriter.</param>
	/// <param name="value">IPagedList.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	public override void Write(Utf8JsonWriter writer, IImmutableList<T> value, JsonSerializerOptions options)
	{
		var wrapper = new ImmutableListJsonWrapper<T>(value.AsEnumerable());
		JsonSerializer.Serialize(writer, wrapper, options);
	}
}
