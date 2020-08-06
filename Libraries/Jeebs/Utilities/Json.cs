﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Util.JsonConverters;

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
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				};

				opt.Converters.Add(new EnumJsonConverterFactory());
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
			=> obj switch
			{
				T x => JsonSerializer.Serialize(x, opt ?? DefaultSettings),
				_ => "{ }"
			};

		/// <summary>
		/// Use JsonSerializer to deserialise a given string into a given object type
		/// </summary>
		/// <typeparam name="T">The type of the object to return</typeparam>
		/// <param name="str">The string to deserialise</param>
		/// <param name="opt">[Optional] JsonSerializerOptions</param>
		/// <returns>Deserialised object of given type</returns>
		public static T Deserialise<T>(string str, JsonSerializerOptions? opt = null)
			=> JsonSerializer.Deserialize<T>(str, opt ?? DefaultSettings);

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
