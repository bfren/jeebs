// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter for <see cref="DateTimeInt"/> objects.
/// </summary>
internal sealed class DateTimeIntJsonConverter : JsonConverter<DateTimeInt>
{
	public override bool HandleNull =>
		true;

	/// <summary>
	/// Read value as UTC DateTime.
	/// </summary>
	/// <param name="reader">Utf8JsonReader.</param>
	/// <param name="typeToConvert">Type.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <returns>Parsed DateTime or <see cref="DateTime.MinValue"/>.</returns>
	public override DateTimeInt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		M.ParseInt64(reader.GetString()).Match(
			fNone: () => DateTimeInt.MinValue,
			fSome: x => new DateTimeInt(x)
		);

	/// <summary>
	/// Convert to UTC and then to a sortable ('s') formatted string.
	/// </summary>
	/// <param name="writer">Utf8JsonWriter.</param>
	/// <param name="value">DateTime.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	public override void Write(Utf8JsonWriter writer, DateTimeInt value, JsonSerializerOptions options) =>
		writer.WriteStringValue(value.ToString());
}
