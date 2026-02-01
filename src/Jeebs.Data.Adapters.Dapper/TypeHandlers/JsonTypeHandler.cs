// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using Jeebs.Functions;

namespace Jeebs.Data.Adapters.Dapper.TypeHandlers;

/// <summary>
/// JSON TypeHandler.
/// </summary>
/// <typeparam name="T">Type to serialise from / deserialise to.</typeparam>
public class JsonTypeHandler<T> : global::Dapper.SqlMapper.StringTypeHandler<T>
{
	/// <summary>
	/// Serialise object to JSON.
	/// </summary>
	/// <param name="xml">T value.</param>
	protected override string Format(T xml) =>
		JsonF.Serialise(xml).Unwrap(_ => JsonF.Empty);

	/// <summary>
	/// Deserialise JSON string.
	/// </summary>
	/// <param name="xml">JSON string.</param>
	protected override T Parse(string xml) =>
		JsonF.Deserialise<T>(xml).Unwrap(_ => throw new JsonException($"Unable to deserialise JSON for {typeof(T)}."));
}
