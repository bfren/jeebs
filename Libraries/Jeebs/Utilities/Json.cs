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
					IgnoreNullValues = true,
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
				};

				opt.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

				return opt;
			}
		}

		/// <summary>
		/// Use JsonSerializer to serialise a given object
		/// Returns '{ }' if <paramref name="obj"/> is null
		/// </summary>
		/// <typeparam name="T">Object Type to be serialised</typeparam>
		/// <param name="obj">The object to serialise</param>
		/// <param name="opt">Serialiser settings</param>
		/// <returns>Json of serialised object</returns>
		public static string Serialise<T>(in T obj, in JsonSerializerOptions? opt = null)
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
		/// <exception cref="ArgumentNullException">If <paramref name="type"/> is null</exception>
		/// <returns>Deserialised object of given type</returns>
		public static object Deserialise(in string str, in Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return JsonSerializer.Deserialize(str, type);
		}

		/// <summary>
		/// Use JsonSerializer to deserialise a given string into a given object type
		/// If 'str' is null, default(T) will be returned (null for reference types, 0 for integers)
		/// </summary>
		/// <typeparam name="T">The type of the object to return</typeparam>
		/// <param name="str">The string to deserialise</param>
		/// <returns>Deserialised object of given type</returns>
		public static T Deserialise<T>(in string str) => (T)Deserialise(str, typeof(T));
	}
}
