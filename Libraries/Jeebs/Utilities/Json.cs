using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeebs.Util
{
	/// <summary>
	/// JSON shorthands
	/// </summary>
	public static class Json
	{
		/// <summary>
		/// Default JsonSerializerOptions
		/// </summary>
		public static JsonSerializerOptions DefaultSettings
		{
			get
			{
				var opt = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					IgnoreNullValues = true
				};

				opt.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

				return opt;
			}
		}



		/// <summary>
		/// Use JsonSerializer to serialise a given object
		/// </summary>
		/// <typeparam name="T">Object Type to be serialised</typeparam>
		/// <param name="obj">The object to serialise</param>
		/// <param name="opt">[Optional] JsonSerializerOptions</param>
		/// <returns>Json String of serialised object</returns>
		public static string Serialise<T>(T obj, JsonSerializerOptions? opt = null)
		{
			if (obj is null)
			{
				return "{ }";
			}

			return JsonSerializer.Serialize(obj, opt ?? DefaultSettings);
		}

		/// <summary>
		/// Use JsonSerializer to deserialise a given string into a given object type
		/// </summary>
		/// <param name="str">The string to deserialise</param>
		/// <param name="type">Object Type</param>
		/// <param name="opt">[Optional] JsonSerializerOptions</param>
		/// <exception cref="ArgumentNullException">If <paramref name="type"/> is null</exception>
		/// <returns>Deserialised object of given type</returns>
		public static object Deserialise(string str, Type type, JsonSerializerOptions? opt = null)
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				throw new ArgumentNullException(nameof(str));
			}

			if (type is null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return JsonSerializer.Deserialize(str, type, opt ?? DefaultSettings);
		}

		/// <summary>
		/// Use JsonSerializer to deserialise a given string into a given object type
		/// If 'str' is null, default(T) will be returned (null for reference types, 0 for integers)
		/// </summary>
		/// <typeparam name="T">The type of the object to return</typeparam>
		/// <param name="str">The string to deserialise</param>
		/// <param name="opt">[Optional] JsonSerializerOptions</param>
		/// <returns>Deserialised object of given type</returns>
		public static T Deserialise<T>(string str, JsonSerializerOptions? opt = null) => (T)Deserialise(str, typeof(T), opt);
	}
}
