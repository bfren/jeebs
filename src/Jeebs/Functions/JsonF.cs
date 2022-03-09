// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Maybe;
using Maybe.Functions;
using Jeebs.Messages;

namespace Jeebs.Functions;

/// <summary>
/// JSON functions
/// </summary>
public static class JsonF
{
	/// <summary>
	/// Empty JSON
	/// </summary>
	public static string Empty { get; } = "\"\"";

	/// <summary>
	/// Default JsonSerializerOptions
	/// </summary>
	private static JsonSerializerOptions Options { get; }

	/// <summary>
	/// Define default settings
	/// </summary>
	static JsonF()
	{
		Options = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.Never,
			DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			NumberHandling = JsonNumberHandling.AllowReadingFromString
		};

		Options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
		Options.Converters.Add(new Internals.EnumeratedConverterFactory());
		Options.Converters.Add(new Internals.MaybeConverterFactory());
		Options.Converters.Add(new Internals.StrongIdConverterFactory());
	}

	/// <summary>
	/// Get a copy of the default serialiser options
	/// </summary>
	public static JsonSerializerOptions CopyOptions()
	{
		var copy = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = Options.DefaultIgnoreCondition,
			DictionaryKeyPolicy = Options.DictionaryKeyPolicy,
			PropertyNamingPolicy = Options.PropertyNamingPolicy,
			NumberHandling = Options.NumberHandling
		};

		foreach (var item in Options.Converters)
		{
			copy.Converters.Add(item);
		}

		return copy;
	}

	/// <summary>
	/// Use JsonSerializer to serialise a given object
	/// </summary>
	/// <typeparam name="T">Object Type to be serialised</typeparam>
	/// <param name="obj">The object to serialise</param>
	/// <param name="options">JsonSerializerOptions</param>
	public static Maybe<string> Serialise<T>(T obj, JsonSerializerOptions options) =>
		obj switch
		{
			T x =>
				MaybeF.Some(
					() => JsonSerializer.Serialize(x, options),
					e => new M.SerialiseExceptionMsg(e)
				),

			_ =>
				Empty
		};

	/// <inheritdoc cref="Serialise{T}(T, JsonSerializerOptions)"/>
	public static Maybe<string> Serialise<T>(T obj) =>
		Serialise(obj, Options);

	/// <summary>
	/// Use JsonSerializer to deserialise a given string into a given object type
	/// </summary>
	/// <typeparam name="T">The type of the object to return</typeparam>
	/// <param name="str">The string to deserialise</param>
	/// <param name="options">JsonSerializerOptions</param>
	public static Maybe<T> Deserialise<T>(string str, JsonSerializerOptions options)
	{
		// Check for null string
		if (str is null || string.IsNullOrWhiteSpace(str))
		{
			return MaybeF.None<T, M.DeserialisingNullOrEmptyStringMsg>();
		}

		// Attempt to deserialise JSON
		try
		{
			return JsonSerializer.Deserialize<T>(str, options) switch
			{
				T x =>
					x,

				_ =>
					MaybeF.None<T, M.DeserialisingReturnedNullMsg>() // should never get here
			};
		}
		catch (Exception ex)
		{
			return MaybeF.None<T>(new M.DeserialiseExceptionMsg(ex));
		}
	}

	/// <inheritdoc cref="Deserialise{T}(string, JsonSerializerOptions)"/>
	public static Maybe<T> Deserialise<T>(string str) =>
		Deserialise<T>(str, Options);

	/// <summary>
	/// Return lower-case boolean string
	/// </summary>
	/// <param name="value">Value</param>
	public static string Bool(bool value) =>
		value switch
		{
			true =>
				"true",

			false =>
				"false"
		};

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Exception caught during <see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions?)"/></summary>
		/// <param name="Value">Exception object</param>
		public sealed record class DeserialiseExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>A null or empty string cannot be deserialised</summary>
		public sealed record class DeserialisingNullOrEmptyStringMsg : Msg;

		/// <summary>The object was deserialised but returned null</summary>
		public sealed record class DeserialisingReturnedNullMsg : Msg;

		/// <summary>Exception caught during <see cref="JsonSerializer.Serialize{TValue}(TValue, JsonSerializerOptions?)"/></summary>
		/// <param name="Value">Exception object</param>
		public sealed record class SerialiseExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
