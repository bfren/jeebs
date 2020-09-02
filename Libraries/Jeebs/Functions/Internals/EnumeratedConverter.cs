using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Jeebs;
using Newtonsoft.Json;

namespace F.Internals
{
	/// <summary>
	/// Converter for custom Enumerated types
	/// </summary>
	/// <typeparam name="T">Enumerated type</typeparam>
	public class EnumeratedConverter<T> : JsonConverter<T>
		where T : Enumerated
	{
		/// <summary>
		/// Read a string value as Enumerated type
		/// </summary>
		/// <param name="reader">JsonReader</param>
		/// <param name="objectType">Enumerated type</param>
		/// <param name="existingValue">Existing value</param>
		/// <param name="hasExistingValue">Whether or not there is an existing value</param>
		/// <param name="serializer">JsonSerializer</param>
		public override T ReadJson(JsonReader reader, Type objectType, [AllowNull] T existingValue, bool hasExistingValue, JsonSerializer serializer)
			=> (T)Activator.CreateInstance(objectType, args: reader.Value?.ToString());

		/// <summary>
		/// Write an Enumerated type value
		/// </summary>
		/// <param name="writer">JsonWriter</param>
		/// <param name="value">Value to write</param>
		/// <param name="serializer">JsonSerializer</param>
		public override void WriteJson(JsonWriter writer, [AllowNull] T value, JsonSerializer serializer)
			=> writer.WriteValue(value?.ToString());
	}
}
