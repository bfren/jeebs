// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using Jeebs;

namespace F
{
	/// <summary>
	/// Boolean functions
	/// </summary>
	public static class BooleanF
	{
		/// <summary>
		/// Parse a boolean value
		/// </summary>
		/// <param name="value">Value to parse</param>
		/// <returns>True / false</returns>
		public static Option<bool> Parse<T>(T value)
		{
			// Convert to string
			var val = value?.ToString()?.ToLower();
			if (val is null)
			{
				return Option.None<bool>(new Jm.Functions.BooleanF.NullValueMsg());
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
			else if (bool.TryParse(val, out bool result))
			{
				return result;
			}

			return Option.None<bool>(new Jm.Functions.BooleanF.UnrecognisedValueMsg(val));
		}
	}
}
