﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.TypeHandlers;

/// <summary>
/// EnumeratedList TypeHandler
/// </summary>
/// <typeparam name="T">Enumerated type</typeparam>
public sealed class EnumeratedListTypeHandler<T> : JsonTypeHandler<EnumeratedList<T>>
	where T : Enumerated
{
	/// <summary>
	/// Parse from list of string values and convert
	/// </summary>
	/// <param name="json">JSON string</param>
	protected override EnumeratedList<T> Parse(string json) =>
		EnumeratedList<T>.Deserialise(json);
}
