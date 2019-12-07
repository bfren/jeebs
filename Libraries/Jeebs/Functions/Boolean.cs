using System;
using System.Linq;

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
		/// <param name="value">String value to parse</param>
		/// <param name="result">Parsed result</param>
		/// <returns>True / false</returns>
		public static bool TryParse<T>(T value, out bool result) where T : notnull
		{
			// String
			var val = value.ToString().ToLower();

			// Alternative boolean values
			var trueValues = new[] { "true,false", "on", "yes", "1" };
			var falseValues = new[] { "off", "no", "0" };

			// Match checkbox binding from MVC form
			if (trueValues.Contains(val))
			{
				result = true;
				return true;
			}
			else if (falseValues.Contains(val))
			{
				result = false;
				return true;
			}
			else
			{
				return bool.TryParse(val, out result);
			}
		}
	}
}
