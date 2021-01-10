using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs;
using Jeebs.Functions.Internals;
using Jm.Functions.JsonF;

namespace F
{
	/// <summary>
	/// JSON shorthands
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

			options.Converters.Add(new EnumeratedConverterFactory());
			options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
		}

		/// <summary>
		/// Get a copy of the default serialiser options
		/// </summary>
		public static JsonSerializerOptions GetOptions()
		{
			var options = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = JsonF.options.DefaultIgnoreCondition,
				DictionaryKeyPolicy = JsonF.options.DictionaryKeyPolicy,
				PropertyNamingPolicy = JsonF.options.PropertyNamingPolicy,
				NumberHandling = JsonF.options.NumberHandling
			};

			foreach (var item in JsonF.options.Converters)
			{
				options.Converters.Add(item);
			}

			return options;
		}

		/// <summary>
		/// Use JsonSerializer to serialise a given object
		/// </summary>
		/// <typeparam name="T">Object Type to be serialised</typeparam>
		/// <param name="obj">The object to serialise</param>
		/// <param name="options">[Optional] JsonSerializerOptions</param>
		/// <returns>Json String of serialised object</returns>
		public static string Serialise<T>(T obj, JsonSerializerOptions? options = null)
			=> obj switch
			{
				T x => JsonSerializer.Serialize(x, options ?? JsonF.options),
				_ => Empty
			};

		/// <summary>
		/// Use JsonSerializer to deserialise a given string into a given object type
		/// </summary>
		/// <typeparam name="T">The type of the object to return</typeparam>
		/// <param name="str">The string to deserialise</param>
		/// <param name="options">[Optional] JsonSerializerOptions</param>
		/// <returns>Deserialised object of given type</returns>
		public static Option<T> Deserialise<T>(string str, JsonSerializerOptions? options = null)
		{
			// Check for null string
			if (str is null || string.IsNullOrWhiteSpace(str))
			{
				return Option.None<T>().AddReason<DeserialisingNullOrEmptyStringMsg>();
			}

			// Attempt to deserialise JSON
			try
			{
				return JsonSerializer.Deserialize<T>(str, options ?? JsonF.options) switch
				{
					T x => x,
					_ => Option.None<T>().AddReason<DeserialisingReturnedNullMsg>() // should never get here
				};
			}
			catch (Exception ex)
			{
				return Option.None<T>().AddReason<DeserialiseExceptionMsg>(ex);
			}
		}

		/// <summary>
		/// Clone an object using JSON
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="obj">Object to clone</param>
		public static T Clone<T>(this T obj)
		{
			var json = Serialise(obj);
			return Deserialise<T>(json).Unwrap(() => throw new JsonException("Unable to clone object."));
		}
	}
}
