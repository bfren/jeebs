// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Collections;

namespace Jeebs.Functions;

public static partial class DictionaryF
{
	/// <summary>
	/// Convert an anonymous object into a dictionary where property names are the keys.
	/// </summary>
	/// <param name="obj">Anonymous object</param>
	/// <returns>ImmutableDictionary object</returns>
	public static ImmutableDictionary<string, object> FromObject(object obj)
	{
		// Create empty dictionary
		var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		if (obj is null)
		{
			return new(dict);
		}

		// Add non-null properties to the dictionary
		foreach (var info in obj.GetType().GetProperties())
		{
			if (info.GetValue(obj) is object value)
			{
				dict.Add(info.Name, value);
			}
		}

		// Return dictionary
		return new(dict);
	}
}
