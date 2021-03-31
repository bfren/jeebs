// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace F.Internals
{
	/// <summary>
	/// StrongId Converter Factory
	/// </summary>
	public sealed class StrongIdConverterFactory : JsonConverterFactory
	{
		/// <summary>
		/// Returns true if <paramref name="typeToConvert"/> inherits from <see cref="Jeebs.StrongId"/>
		/// </summary>
		/// <param name="typeToConvert">Type to convert</param>
		public override bool CanConvert(Type typeToConvert) =>
			typeToConvert.IsSubclassOf(typeof(Jeebs.StrongId));

		/// <summary>
		/// Creates JsonConverter using StrongId type as generic argument
		/// </summary>
		/// <param name="typeToConvert">StrongId type</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var converterType = typeof(StrongIdConverter<>).MakeGenericType(typeToConvert);
			return Activator.CreateInstance(converterType) switch
			{
				JsonConverter x =>
					x,

				_ =>
					throw new JsonException($"Unable to create {typeof(StrongIdConverter<>)} for type {typeToConvert}.")
			};
		}
	}
}
