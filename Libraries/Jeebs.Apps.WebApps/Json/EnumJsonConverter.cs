using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeebs.Apps.WebApps.Json
{
	/// <summary>
	/// Converter for custom Enum types
	/// </summary>
	/// <typeparam name="T">Enum type</typeparam>
	public class EnumJsonConverter<T> : JsonConverter<T>
		where T : Enum
	{
		/// <summary>
		/// Read a string value as Enum type
		/// </summary>
		/// <param name="reader">Utf8JsonReader</param>
		/// <param name="typeToConvert">Enum type</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> Activator.CreateInstance(typeToConvert, args: reader.GetString()) switch
			{
				T x => x,
				_ => throw new JsonException($"Unable to create Enum type {typeof(T)} from JSON.")
			};

		/// <summary>
		/// Write an Enum type value
		/// </summary>
		/// <param name="writer">Utf8JsonWriter</param>
		/// <param name="value">Enum type</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
			=> writer.WriteStringValue(value.ToString());
	}
}
