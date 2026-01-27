// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;

namespace Jeebs.Functions;

public static partial class ListF
{
	/// <summary>
	/// Deserialise a JSON list into an ImmutableList.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="json">JSON list.</param>
	/// <returns>Deserialised object.</returns>
	public static ImmutableList<T> Deserialise<T>(string json) =>
		JsonF.Deserialise<List<T>>(json)
			.Match(
				fail: _ => new(),
				ok: x => Create(items: x)
			);
}
