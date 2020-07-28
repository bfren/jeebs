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
			where T : notnull
		{
			// String
			var val = value.ToString().ToLower();

			// Alternative boolean values
			var trueValues = new[] { "true,false", "on", "yes", "1" };
			var falseValues = new[] { "off", "no", "0" };

			// Match checkbox binding from MVC form
			if (trueValues.Contains(val))
			{
				return Option.Some(true);
			}
			else if (falseValues.Contains(val))
			{
				return Option.Some(false);
			}
			else if (bool.TryParse(val, out bool result))
			{
				return Option.Some(result);
			}

			return Option.None<bool>();
		}
	}
}
