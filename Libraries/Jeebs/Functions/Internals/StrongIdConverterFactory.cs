// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeebs.Functions.Internals
{
	/// <summary>
	/// StrongId Converter Factory
	/// </summary>
	public sealed class StrongIdConverterFactory : JsonConverterFactory
	{
		/// <summary>
		/// Returns true if <paramref name="typeToConvert"/> inherits from <see cref="StrongId{T}"/>
		/// </summary>
		/// <param name="typeToConvert">Type to convert</param>
		public override bool CanConvert(Type typeToConvert) =>
			typeToConvert.Implements(typeof(StrongId<>));

		/// <summary>
		/// Creates JsonConverter for StrongID
		/// </summary>
		/// <param name="typeToConvert">StrongID type</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			// Get converter type
			var converterType = typeToConvert switch
			{
				Type t when t.Implements<GuidId>() =>
					typeof(GuidIdConverter<>).MakeGenericType(typeToConvert),

				Type t when t.Implements<IntId>() =>
					typeof(IntIdConverter<>).MakeGenericType(typeToConvert),

				Type t when t.Implements<LongId>() =>
					typeof(LongIdConverter<>).MakeGenericType(typeToConvert),

				_ =>
					throw new JsonException($"Unknown StrongId<> type: {typeToConvert}.")
			};

			// Create converter
			return Activator.CreateInstance(converterType) switch
			{
				JsonConverter x =>
					x,

				_ =>
					throw new JsonException($"Unable to create {converterType} for type {typeToConvert}.")
			};
		}
	}
}
