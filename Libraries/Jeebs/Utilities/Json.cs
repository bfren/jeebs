using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jeebs.Util
{
	/// <summary>
	/// JSON shorthands
	/// </summary>
	public static class Json
	{
		/// <summary>
		/// Empty JSON string
		/// </summary>
		public const string Empty = "{ }";

		/// <summary>
		/// Default JsonSerializerSettings
		/// </summary>
		public static JsonSerializerSettings DefaultSettings => new JsonSerializerSettings
		{
			ContractResolver = new JeebsContractResolver(),
			NullValueHandling = NullValueHandling.Ignore
		};

		/// <summary>
		/// Use JsonSerializer to serialise a given object
		/// </summary>
		/// <typeparam name="T">Object Type to be serialised</typeparam>
		/// <param name="obj">The object to serialise</param>
		/// <param name="opt">[Optional] JsonSerializerSettings</param>
		/// <returns>Json String of serialised object</returns>
		public static string Serialise<T>(T obj, JsonSerializerSettings? opt = null)
			=> obj switch
			{
				T x => JsonConvert.SerializeObject(x, opt ?? DefaultSettings),
				_ => Empty
			};

		/// <summary>
		/// Use JsonSerializer to deserialise a given string into a given object type
		/// </summary>
		/// <typeparam name="T">The type of the object to return</typeparam>
		/// <param name="str">The string to deserialise</param>
		/// <param name="ifNull">[Optional] Function to return a default <typeparamref name="T"/> if deserialise returns null - if not set a <see cref="JsonException"/> will be thrown</param>
		/// <param name="opt">[Optional] JsonSerializerSettings</param>
		/// <returns>Deserialised object of given type</returns>
		public static T Deserialise<T>(string str, Func<T>? ifNull = null, JsonSerializerSettings? opt = null)
			=> JsonConvert.DeserializeObject<T>(str.Trim(), opt ?? DefaultSettings) switch
			{
				T x => x,
				_ => ifNull switch
				{
					Func<T> y => y(),
					_ => throw new JsonException($"Null return when deserialising JSON: {str}")
				}
			};

		/// <summary>
		/// Clone an object using JSON
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="obj">Object to clone</param>
		public static T Clone<T>(this T obj)
		{
			var json = Serialise(obj);
			return Deserialise<T>(json);
		}
	}
}
