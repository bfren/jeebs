// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Messages;

namespace Jeebs.Functions.JsonConverters;

/// <summary>
/// <see cref="Maybe{T}"/> JSON converter
/// </summary>
/// <typeparam name="T">Maybe value type</typeparam>
internal sealed class MaybeJsonConverter<T> : JsonConverter<Maybe<T>>
{
	/// <summary>
	/// Read value and return as <see cref="MaybeF.Internals.Some{T}"/> or <see cref="MaybeF.Internals.None{T}"/>
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
	/// If the Maybe is <see cref="MaybeF.Internals.Some{T}"/> write the value, otherwise write a null value
	/// </summary>
	/// <param name="writer">Utf8JsonWriter</param>
	/// <param name="value">Maybe value</param>
	/// <param name="options">JsonSerializerOptions</param>
	public override void Write(Utf8JsonWriter writer, Maybe<T> value, JsonSerializerOptions options)
	{
		if (value.IsSome(out var obj))
		{
			JsonSerializer.Serialize(writer, obj, options);
		}
		else
		{
			writer.WriteStringValue(string.Empty);
		}
	}
}

/// <summary>
/// Wrapper for <see cref="MaybeJsonConverter{T}"/> messages because it has a generic constraint
/// </summary>
internal static class M
{
	/// <summary>Deserialisation returned a null value</summary>
	public sealed record class DeserialisingReturnedNullMsg() : Msg;
}
