// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter for <see cref="ImmutableList{T}"/> objects. Round-trips as a plain JSON array.
/// </summary>
/// <typeparam name="T">Item type.</typeparam>
internal sealed class ImmutableListJsonConverter<T> : JsonConverter<ImmutableList<T>>
{
	/// <summary>
	/// Read an <see cref="ImmutableList{T}"/> from a JSON array.
	/// </summary>
	/// <exception cref="JsonException"/>
	public override ImmutableList<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Null)
		{
			return null;
		}

		if (reader.TokenType != JsonTokenType.StartArray)
		{
			throw new JsonException($"Expected StartArray for {typeof(ImmutableList<T>)}, got {reader.TokenType}.");
		}

		var items = JsonSerializer.Deserialize<List<T>>(ref reader, options);
		return new ImmutableList<T>(items ?? []);
	}

	/// <summary>
	/// Write an <see cref="ImmutableList{T}"/> as a JSON array.
	/// </summary>
	public override void Write(Utf8JsonWriter writer, ImmutableList<T> value, JsonSerializerOptions options) =>
		JsonSerializer.Serialize(writer, value.AsEnumerable(), options);
}
