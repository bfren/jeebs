// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs;
using static F.OptionF;

namespace F;

/// <summary>
/// JSON functions
/// </summary>
public static class JsonF
{
	/// <summary>
	/// Empty JSON
	/// </summary>
	public const string Empty = "\"\"";

	/// <summary>
	/// Default JsonSerializerOptions
	/// </summary>
	private static readonly JsonSerializerOptions options;

	/// <summary>
	/// Define default settings
	/// </summary>
	static JsonF()
	{
		options = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
			DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			NumberHandling = JsonNumberHandling.AllowReadingFromString
		};

		options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
		options.Converters.Add(new Internals.EnumeratedConverterFactory());
		options.Converters.Add(new Internals.OptionConverterFactory());
		options.Converters.Add(new Internals.StrongIdConverterFactory());
	}

	/// <summary>
	/// Get a copy of the default serialiser options
	/// </summary>
	public static JsonSerializerOptions CopyOptions()
	{
		var copy = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = options.DefaultIgnoreCondition,
			DictionaryKeyPolicy = options.DictionaryKeyPolicy,
			PropertyNamingPolicy = options.PropertyNamingPolicy,
			NumberHandling = options.NumberHandling
		};

		foreach (var item in options.Converters)
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
	/// <returns>Json String of serialised object</returns>
	public static Option<string> Serialise<T>(T obj, JsonSerializerOptions options) =>
		obj switch
		{
			T x =>
				Some(
					() => JsonSerializer.Serialize(x, options),
					e => new Msg.SerialiseExceptionMsg(e)
				),

			_ =>
				Empty
		};

	/// <inheritdoc cref="Serialise{T}(T, JsonSerializerOptions)"/>
	public static Option<string> Serialise<T>(T obj) =>
		Serialise(obj, options);

	/// <summary>
	/// Use JsonSerializer to deserialise a given string into a given object type
	/// </summary>
	/// <typeparam name="T">The type of the object to return</typeparam>
	/// <param name="str">The string to deserialise</param>
	/// <param name="options">JsonSerializerOptions</param>
	/// <returns>Deserialised object of given type</returns>
	public static Option<T> Deserialise<T>(string str, JsonSerializerOptions options)
	{
		// Check for null string
		if (str is null || string.IsNullOrWhiteSpace(str))
		{
			return None<T, Msg.DeserialisingNullOrEmptyStringMsg>();
		}

		// Attempt to deserialise JSON
		try
		{
			return JsonSerializer.Deserialize<T>(str, options) switch
			{
				T x =>
					x,

				_ =>
					None<T, Msg.DeserialisingReturnedNullMsg>() // should never get here
			};
		}
		catch (Exception ex)
		{
			return None<T>(new Msg.DeserialiseExceptionMsg(ex));
		}
	}

	/// <inheritdoc cref="Deserialise{T}(string, JsonSerializerOptions)"/>
	public static Option<T> Deserialise<T>(string str) =>
		Deserialise<T>(str, options);

	/// <summary>Messages</summary>
	public static class Msg
	{
		/// <summary>Exception caught during <see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions?)"/></summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class DeserialiseExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>A null or empty string cannot be deserialised</summary>
		public sealed record class DeserialisingNullOrEmptyStringMsg : IMsg { }

		/// <summary>The object was deserialised but returned null</summary>
		public sealed record class DeserialisingReturnedNullMsg : IMsg { }

		/// <summary>Exception caught during <see cref="JsonSerializer.Serialize{TValue}(TValue, JsonSerializerOptions?)"/></summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class SerialiseExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
	}
}
