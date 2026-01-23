// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Functions;

public static partial class DictionaryF
{
	/// <summary>
	/// Convert an anonymous object into a dictionary where property names are the keys.
	/// </summary>
	/// <param name="obj">Anonymous object.</param>
	/// <returns>Dictionary object.</returns>
	public static Dictionary<string, object> FromObject(object? obj)
	{
		// Return empty dictionary
		if (obj is null)
		{
			return [];
		}

		// Add non-null properties to the dictionary
		var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		foreach (var info in obj.GetType().GetProperties())
		{
			if (info.GetValue(obj) is object value)
			{
				dict.Add(info.Name.ToLowerInvariant(), value);
			}
		}

		// Return dictionary
		return dict;
	}
}
