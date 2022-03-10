// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Messages;
using MaybeF;
using MaybeF.Internals;

namespace Jeebs.Functions.Internals;

/// <summary>
/// Convert an <see cref="Maybe{T}"/> to and from JSON
/// </summary>
/// <typeparam name="T">Maybe value type</typeparam>
public sealed class MaybeConverter<T> : JsonConverter<Maybe<T>>
{
	/// <summary>
	/// Read value and return as <see cref="Some{T}"/> or <see cref="None{T}"/>
	/// </summary>
	/// <param name="reader">Utf8JsonReader</param>
	/// <param name="typeToConvert">Maybe of type <typeparamref name="T"/></param>
	/// <param name="options">JsonSerializerOptions</param>
	public override Maybe<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		JsonSerializer.Deserialize<T>(ref reader, options) switch
		{
			T value =>
				value,

			_ =>
				F.None<T, M.DeserialisingReturnedNullMsg>() // should never get here
		};

	/// <summary>
	/// If the Maybe is <see cref="Some{T}"/> write the value, otherwise write a null value
	/// </summary>
	/// <param name="writer">Utf8JsonWriter</param>
	/// <param name="value">Maybe value</param>
	/// <param name="options">JsonSerializerOptions</param>
	public override void Write(Utf8JsonWriter writer, Maybe<T> value, JsonSerializerOptions options)
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
/// Wrapper for <see cref="MaybeConverter{T}"/> messages because it has a generic constraint
/// </summary>
public static class M
{
	/// <summary>Deserialisation returned a null value</summary>
	public sealed record class DeserialisingReturnedNullMsg() : Msg;
}
