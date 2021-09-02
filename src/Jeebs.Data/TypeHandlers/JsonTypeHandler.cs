// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using static F.JsonF;

namespace Jeebs.Data.TypeHandlers
{
	/// <summary>
	/// JSON TypeHandler
	/// </summary>
	/// <typeparam name="T">Type to serialise from / deserialise to</typeparam>
	public class JsonTypeHandler<T> : Dapper.SqlMapper.StringTypeHandler<T>
	{
		/// <summary>
		/// Serialise object to JSON
		/// </summary>
		/// <param name="value">T value</param>
		/// <returns>JSON</returns>
		protected override string Format(T value) =>
			Serialise(value).Unwrap(Empty);

		/// <summary>
		/// Deserialise JSON string
		/// </summary>
		/// <param name="json">JSON string</param>
		protected override T Parse(string json) =>
			Deserialise<T>(json).Unwrap(() => throw new JsonException($"Unable to deserialise JSON for {typeof(T)}."));
	}
}
