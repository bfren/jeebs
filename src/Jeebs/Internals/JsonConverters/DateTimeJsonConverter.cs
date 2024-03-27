// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter for <see cref="DateTime"/> objects.
/// </summary>
internal sealed class DateTimeJsonConverter : JsonConverter<DateTime>
{
	/// <summary>
	/// Read value as UTC DateTime.
	/// </summary>
	/// <param name="reader">Utf8JsonReader.</param>
	/// <param name="typeToConvert">Type.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <returns>Parsed DateTime or <see cref="DateTime.MinValue"/>.</returns>
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		M.ParseDateTime(reader.GetString()).Match(
			none: DateTime.MinValue.ToUniversalTime,
			some: x => x.ToUniversalTime()
		);

	/// <summary>
	/// Convert to UTC and then to a sortable ('s') formatted string.
	/// </summary>
	/// <param name="writer">Utf8JsonWriter.</param>
	/// <param name="value">DateTime.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
		writer.WriteStringValue(value.ToUniversalTime().ToString("s"));
}
