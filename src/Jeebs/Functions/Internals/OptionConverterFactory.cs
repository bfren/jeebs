﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs;

namespace F.Internals;

/// <summary>
/// Maybe Converter Factory
/// </summary>
public sealed class OptionConverterFactory : JsonConverterFactory
{
	/// <summary>
	/// Returns true if <paramref name="typeToConvert"/> inherits from <see cref="Maybe{T}"/>
	/// </summary>
	/// <param name="typeToConvert">Type to convert</param>
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.Implements(typeof(Maybe<>));

	/// <summary>
	/// Creates JsonConverter for <see cref="Maybe{T}"/>
	/// </summary>
	/// <param name="typeToConvert">Maybe type</param>
	/// <param name="options">JsonSerializerOptions</param>
	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		// Get converter type
		var wrappedType = typeToConvert.GetGenericArguments()[0];
		var converterType = typeof(OptionConverter<>).MakeGenericType(wrappedType);

		// Create converter
		return Activator.CreateInstance(converterType) switch
		{
			JsonConverter x =>
				x,

			_ =>
				throw new JsonException($"Unable to create {converterType} for type {typeToConvert}.")
		};
	}
}
