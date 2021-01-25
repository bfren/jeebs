using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Jeebs.Functions.Internals
{
	/// <summary>
	/// Converter for <see cref="IntId"/> types
	/// </summary>
	/// <typeparam name="T">IntId type</typeparam>
	internal class IntIdConverter<T> : StrongIdConverter<IntId, int>
		where T : IntId, new()
	{
		/// <summary>
		/// Create object
		/// </summary>
		public IntIdConverter() : base(0) { }

		/// <summary>
		/// Read an IntId type value
		/// </summary>
		/// <param name="reader">Utf8JsonReader</param>
		/// <param name="typeToConvert">IntId type</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			new T()
			{
				Value = reader.TokenType switch
				{
					// Handle numbers
					JsonTokenType.Number =>
						reader.GetInt32(),

					// Handle strings
					JsonTokenType.String =>
						HandleString(
							ref reader,
							_ => AllowStringsAsNumbers(options),
							int.TryParse
						),

					// Handle default
					_ =>
						HandleDefault(ref reader)
				}
			};
	}
}
