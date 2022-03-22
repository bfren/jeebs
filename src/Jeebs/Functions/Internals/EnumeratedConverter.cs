// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeebs.Functions.Internals;

/// <summary>
/// <see cref="Enumerated"/> JSON converter
/// </summary>
/// <typeparam name="T">Enumerated type</typeparam>
internal sealed class EnumeratedConverter<T> : JsonConverter<T>
	where T : Enumerated
{
	/// <summary>
	/// Read an Enumerated type value - which requires the 'name' (value) to be passed in the constructor
	/// </summary>
	/// <param name="reader">Utf8JsonReader</param>
	/// <param name="typeToConvert">Enumerated type</param>
	/// <param name="options">JsonSerializerOptions</param>
	/// <exception cref="JsonException"></exception>
	public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		Activator.CreateInstance(typeToConvert, args: reader.GetString()) switch
		{
			T x =>
				x,

			_ =>
				throw new JsonException($"Unable to create Enum type {typeof(T)} from JSON.")
		};

	/// <summary>
	/// Write an Enumerated type value
	/// </summary>
	/// <param name="writer">Utf8JsonWriter</param>
	/// <param name="value">Enumerated value</param>
	/// <param name="options">JsonSerializerOptions</param>
	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
		writer.WriteStringValue(value.ToString());
}
