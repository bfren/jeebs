// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;

namespace Jeebs.Data.TypeHandlers;

/// <summary>
/// ImmutableList JsonTypeHandler.
/// </summary>
/// <typeparam name="T">Enumerated type</typeparam>
public sealed class ImmutableListJsonTypeHandler<T> : JsonTypeHandler<ImmutableList<T>>
{
	/// <summary>
	/// Parse from list of string values and convert.
	/// </summary>
	/// <param name="xml">JSON string</param>
	protected override ImmutableList<T> Parse(string xml) =>
		ImmutableList.Deserialise<T>(xml);
}
