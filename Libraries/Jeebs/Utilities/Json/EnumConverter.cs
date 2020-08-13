using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Newtonsoft.Json;

namespace Jeebs.Util
{
	/// <summary>
	/// Converter for custom Enum types
	/// </summary>
	/// <typeparam name="T">Enum type</typeparam>
	public class EnumConverter<T> : JsonConverter<T>
		where T : Enum
	{
		/// <summary>
		/// Read a string value as Enum type
		/// </summary>
		/// <param name="reader">JsonReader</param>
		/// <param name="objectType">Type of Enum</param>
		/// <param name="existingValue">Existing value</param>
		/// <param name="hasExistingValue">Whether or not there is an existing value</param>
		/// <param name="serializer">JsonSerializer</param>
		public override T ReadJson(JsonReader reader, Type objectType, [AllowNull] T existingValue, bool hasExistingValue, JsonSerializer serializer)
			=> (T)Activator.CreateInstance(objectType, args: reader.ToString());

		/// <summary>
		/// Write an Enum type value
		/// </summary>
		/// <param name="writer">JsonWriter</param>
		/// <param name="value">Value to write</param>
		/// <param name="serializer">JsonSerializer</param>
		public override void WriteJson(JsonWriter writer, [AllowNull] T value, JsonSerializer serializer)
			=> writer.WriteValue(value?.ToString());
	}
}
