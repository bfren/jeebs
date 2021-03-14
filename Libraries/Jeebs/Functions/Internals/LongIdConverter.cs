// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Text.Json;
using Jeebs.Id;

namespace F.Internals
{
	/// <summary>
	/// Converter for <see cref="LongId"/> types
	/// </summary>
	/// <typeparam name="T">LongId type</typeparam>
	internal class LongIdConverter<T> : StrongIdConverter<LongId, long>
		where T : LongId, new()
	{
		/// <summary>
		/// Create object
		/// </summary>
		public LongIdConverter() : base(0L) { }

		/// <summary>
		/// Read an LongId type value
		/// </summary>
		/// <param name="reader">Utf8JsonReader</param>
		/// <param name="typeToConvert">LongId type</param>
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
					JsonTokenType.String =>
						HandleString(
							ref reader,
							_ => AllowStringsAsNumbers(options),
							long.TryParse
						),

					// Handle default
					_ =>
						HandleDefault(ref reader)
				}
			};
	}
}
