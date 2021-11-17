// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs;
using Jeebs.Internals;
using static F.OptionF;

namespace F.Internals;

/// <summary>
/// Convert an <see cref="Option{T}"/> to and from JSON
/// </summary>
/// <typeparam name="T">Option value type</typeparam>
public sealed class OptionConverter<T> : JsonConverter<Option<T>>
{
	/// <summary>
	/// Read value and return as <see cref="Jeebs.Internals.Some{T}"/> or <see cref="Jeebs.Internals.None{T}"/>
	/// </summary>
	/// <param name="reader">Utf8JsonReader</param>
	/// <param name="typeToConvert">Option of type <typeparamref name="T"/></param>
	/// <param name="options">JsonSerializerOptions</param>
	public override Option<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		JsonSerializer.Deserialize<T>(ref reader, options) switch
		{
			T value =>
				value,

			_ =>
				None<T, M.DeserialisingReturnedNullMsg>() // should never get here
		};

	/// <summary>
	/// If the option is <see cref="Jeebs.Internals.Some{T}"/> write the value, otherwise write a null value
	/// </summary>
	/// <param name="writer">Utf8JsonWriter</param>
	/// <param name="value">Option value</param>
	/// <param name="options">JsonSerializerOptions</param>
	public override void Write(Utf8JsonWriter writer, Option<T> value, JsonSerializerOptions options)
	{
		if (value is Some<T> some)
		{
			JsonSerializer.Serialize(writer, some.Value, options);
		}
		else
		{
			writer.WriteStringValue(string.Empty);
		}
	}
}

/// <summary>
/// Wrapper for <see cref="OptionConverter{T}"/> messages because it has a generic constraint
/// </summary>
public static class M
{
	/// <summary>Deserialisation returned a null value</summary>
	public sealed record class DeserialisingReturnedNullMsg() : Msg;
}
