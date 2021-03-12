// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Id;

namespace F.Internals
{
	/// <summary>
	/// Converter for <see cref="StrongId{T}"/> types
	/// </summary>
	/// <typeparam name="TId">StrongId type</typeparam>
	/// <typeparam name="TValue">StrongId Value type</typeparam>
	internal abstract class StrongIdConverter<TId, TValue> : JsonConverter<TId>
		where TId : StrongId<TValue>
		where TValue : notnull
	{
		private readonly TValue blank;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="blank">Blank value</param>
		protected StrongIdConverter(TValue blank) =>
			this.blank = blank;

		/// <summary>
		/// Write a StrongId type value
		/// </summary>
		/// <param name="writer">Utf8JsonWriter</param>
		/// <param name="id">StrongId</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override void Write(Utf8JsonWriter writer, TId id, JsonSerializerOptions options) =>
			writer.WriteStringValue(id.Value?.ToString());

		/// <summary>
		/// Parse a string value
		/// </summary>
		/// <param name="input">Input string</param>
		/// <param name="value">Output <typeparamref name="TValue"/></param>
		protected delegate bool ParseString(string input, out TValue value);

		/// <summary>
		/// Handle a string value
		/// </summary>
		/// <param name="reader">Utf8JsonReader</param>
		/// <param name="predicate">If returns true, will attempt to parse string</param>
		/// <param name="parse">ParseString</param>
		protected TValue HandleString(ref Utf8JsonReader reader, Func<string, bool> predicate, ParseString parse) =>
			reader.GetString() switch
			{
				string value when predicate(value) =>
					parse(value, out TValue id) switch
					{
						true =>
							id,

						false =>
							blank
					},

				_ =>
					blank
			};

		/// <summary>
		/// Skip value and return blank, or throw <see cref="JsonException"/> if that doesn't work
		/// </summary>
		/// <param name="reader">Utf8JsonReader</param>
		protected TValue HandleDefault(ref Utf8JsonReader reader) =>
			reader.TrySkip() switch
			{
				true =>
					blank,

				false =>
					throw new JsonException($"Invalid {typeof(TValue)} and unable to skip reading current token.")
			};

		/// <summary>
		/// Returns true if <see cref="JsonSerializerOptions.NumberHandling"/> is <see cref="JsonNumberHandling.AllowReadingFromString"/>
		/// </summary>
		/// <param name="options">JsonSerializerOptions</param>
		protected static bool AllowStringsAsNumbers(JsonSerializerOptions options) =>
			(options.NumberHandling & JsonNumberHandling.AllowReadingFromString) != 0;
	}
}
