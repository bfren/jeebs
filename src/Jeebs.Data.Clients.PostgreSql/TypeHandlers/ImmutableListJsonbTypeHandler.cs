// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;

namespace Jeebs.Data.Clients.PostgreSql.TypeHandlers;

/// <summary>
/// ImmutableList JsonTypeHandler.
/// </summary>
/// <typeparam name="T">Enumerated type</typeparam>
public sealed class ImmutableListJsonbTypeHandler<T> : JsonbTypeHandler<ImmutableList<T>>
{
	/// <summary>
	/// Parse from list of string values and convert
	/// </summary>
	/// <param name="value">JSON string</param>
	public override ImmutableList<T> Parse(object value) =>
		value switch
		{
			string json =>
				ImmutableList.Deserialise<T>(json),

			_ =>
				new()
		};
}
