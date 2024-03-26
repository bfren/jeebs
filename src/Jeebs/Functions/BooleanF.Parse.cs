// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;
using System.Linq;

namespace Jeebs.Functions;

/// <summary>
/// Boolean functions.
/// </summary>
public static class BooleanF
{
	/// <summary>
	/// Parse a boolean value.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="value">Value to parse.</param>
	public static Maybe<bool> Parse<T>(T value)
	{
		// Convert to string
		var val = value?.ToString()?.ToLower(CultureInfo.InvariantCulture);
		if (val is null)
		{
			return M.None;
		}

		// Alternative boolean values
		var trueValues = new[] { "true,false", "on", "yes", "1" };
		var falseValues = new[] { "off", "no", "0" };

		// Match checkbox binding from MVC form
		if (trueValues.Contains(val))
		{
			return true;
		}
		else if (falseValues.Contains(val))
		{
			return false;
		}

		// Parse default values
		return M.ParseBool(val);
	}
}
