using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Jeebs.Util
{
	/// <summary>
	/// JSON shorthands
	/// </summary>
	public static class Json
	{
		/// <summary>
		/// Default JSON serialiser settings
		/// </summary>
		public static JsonSerializerOptions DefaultSettings = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			IgnoreNullValues = true
		};

		/// <summary>
		/// Use JsonConvert to serialise a given object
		/// </summary>
		/// <typeparam name="T">Object Type to be serialised</typeparam>
		/// <param name="obj">The object to serialise</param>
		/// <param name="settings">Serialiser settings</param>
		/// <returns>Json String of serialised object</returns>
		public static string Serialise<T>(in T obj, in JsonSerializerOptions? settings = null) =>
			JsonSerializer.Serialize(obj, settings ?? DefaultSettings);

		/// <summary>
		/// Use JsonConvert to deserialise a given string into a given object type
		/// </summary>
		/// <param name="str">The string to deserialise</param>
		/// <param name="type">Object Type</param>
		/// <exception cref="ArgumentNullException">If <paramref name="type"/> is null</exception>
		/// <returns>Deserialised object of given type</returns>
		public static object Deserialise(in string str, in Type type)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException(nameof(str));
			}

			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return JsonSerializer.Deserialize(str, type);
		}

		/// <summary>
		/// Use JsonConvert to deserialise a given string into a given object type
		/// If 'str' is null, default(T) will be returned (null for reference types, 0 for integers)
		/// </summary>
		/// <typeparam name="T">The type of the object to return</typeparam>
		/// <param name="str">The string to deserialise</param>
		/// <returns>Deserialised object of given type</returns>
		public static T Deserialise<T>(in string str) => (T)Deserialise(str, typeof(T));
	}
}
