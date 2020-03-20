using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeebs.Util.JsonConverters
{
	/// <summary>
	/// Converter Factory for custom Enum types
	/// </summary>
	public sealed class EnumJsonConverterFactory : JsonConverterFactory
	{
		/// <summary>
		/// Returns true if <paramref name="typeToConvert"/> inherits from <see cref="Enum"/>
		/// </summary>
		/// <param name="typeToConvert">Type to convert</param>
		public override bool CanConvert(Type typeToConvert) => typeToConvert.IsSubclassOf(typeof(Enum));

		/// <summary>
		/// Creates JsonConverter using Enum type as generic argument
		/// </summary>
		/// <param name="typeToConvert">Enum type</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var converterType = typeof(EnumJsonConverter<>).MakeGenericType(typeToConvert);
			return (JsonConverter)Activator.CreateInstance(converterType);
		}
	}
}
