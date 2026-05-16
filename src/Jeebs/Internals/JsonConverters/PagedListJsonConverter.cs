// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// JSON converter for <see cref="PagedList{T}"/> objects. Round-trips as
/// <c>{ "values": { ...PagingValues... }, "items": [ ... ] }</c>.
/// </summary>
/// <typeparam name="T">Item type.</typeparam>
internal sealed class PagedListJsonConverter<T> : JsonConverter<PagedList<T>>
{
	/// <summary>
	/// Read a <see cref="PagedList{T}"/> from a JSON object.
	/// </summary>
	/// <exception cref="JsonException"/>
	public override PagedList<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Null)
		{
			return null;
		}

		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException($"Expected StartObject for {typeof(PagedList<T>)}, got {reader.TokenType}.");
		}

		var valuesName = options.PropertyNamingPolicy?.ConvertName("Values") ?? "Values";
		var itemsName = options.PropertyNamingPolicy?.ConvertName("Items") ?? "Items";

		PagingValues? values = null;
		List<T>? items = null;

		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndObject)
			{
				return new PagedList<T>(values ?? new PagingValues(), items ?? []);
			}

			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException($"Expected PropertyName in {typeof(PagedList<T>)}, got {reader.TokenType}.");
			}

			var propName = reader.GetString();
			reader.Read();

			if (string.Equals(propName, valuesName, StringComparison.OrdinalIgnoreCase))
			{
				values = JsonSerializer.Deserialize<PagingValues>(ref reader, options);
			}
			else if (string.Equals(propName, itemsName, StringComparison.OrdinalIgnoreCase))
			{
				items = JsonSerializer.Deserialize<List<T>>(ref reader, options);
			}
			else
			{
				reader.Skip();
			}
		}

		throw new JsonException($"Unexpected end of JSON while reading {typeof(PagedList<T>)}.");
	}

	/// <summary>
	/// Write a <see cref="PagedList{T}"/> as a JSON object with <c>values</c> and <c>items</c>.
	/// </summary>
	public override void Write(Utf8JsonWriter writer, PagedList<T> value, JsonSerializerOptions options)
	{
		var valuesName = options.PropertyNamingPolicy?.ConvertName("Values") ?? "Values";
		var itemsName = options.PropertyNamingPolicy?.ConvertName("Items") ?? "Items";

		// Coerce IPagingValues to the concrete PagingValues record so System.Text.Json
		// picks up the init-settable properties for serialisation.
		var concreteValues = value.Values as PagingValues ?? new PagingValues
		{
			Items = value.Values.Items,
			ItemsPer = value.Values.ItemsPer,
			FirstItem = value.Values.FirstItem,
			LastItem = value.Values.LastItem,
			Page = value.Values.Page,
			Pages = value.Values.Pages,
			PagesPer = value.Values.PagesPer,
			LowerPage = value.Values.LowerPage,
			UpperPage = value.Values.UpperPage,
			Skip = value.Values.Skip,
			Take = value.Values.Take
		};

		writer.WriteStartObject();

		writer.WritePropertyName(valuesName);
		JsonSerializer.Serialize(writer, concreteValues, options);

		writer.WritePropertyName(itemsName);
		JsonSerializer.Serialize(writer, value.AsEnumerable(), options);

		writer.WriteEndObject();
	}
}
