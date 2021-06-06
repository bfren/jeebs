// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace F.Internals
{
	/// <summary>
	/// Converter for <see cref="Jeebs.StrongId"/> types
	/// </summary>
	/// <typeparam name="T">StrongId type</typeparam>
	public sealed class StrongIdConverter<T> : JsonConverter<T>
		where T : Jeebs.StrongId, new()
	{
		/// <summary>
		/// Read an Enumerated type value - which requires the 'name' (value) to be passed in the constructor
		/// </summary>
		/// <param name="reader">Utf8JsonReader</param>
		/// <param name="typeToConvert">StrongId type</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			new()
			{
				Value = reader.TokenType switch
				{
					// Handle numbers
					JsonTokenType.Number =>
						reader.GetInt64(),

					// Handle strings if strings are allowed
					JsonTokenType.String when (options.NumberHandling & JsonNumberHandling.AllowReadingFromString) != 0 =>
						long.TryParse(reader.GetString(), out long id) switch
						{
							true =>
								id,

							false =>
								0
						},

					// Handle default
					_ =>
						reader.TrySkip() switch
						{
							true =>
								0,

							false =>
								throw new JsonException($"Invalid {typeof(T)} and unable to skip reading current token.")
						}
				}
			};

		/// <summary>
		/// Write a StrongId type value
		/// </summary>
		/// <param name="writer">Utf8JsonWriter</param>
		/// <param name="value">StrongId value</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
			writer.WriteStringValue(value.Value.ToString());
	}
}
