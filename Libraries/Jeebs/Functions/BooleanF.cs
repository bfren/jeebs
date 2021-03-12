// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using Jeebs;
using Msg = F.BooleanFMsg;
using static F.OptionF;

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
				return None<bool, Msg.NullValueMsg>();
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

			return None<bool>(new Msg.UnrecognisedValueMsg(val));
		}
	}
}

namespace F.BooleanFMsg
{
	/// <summary>Null Value</summary>
	public sealed record NullValueMsg : IMsg { }

	/// <summary>Unrecognised boolean value</summary>
	/// <param name="Value">Unrecognised Value</param>
	public sealed record UnrecognisedValueMsg(string Value) : WithValueMsg<string>() { }
}
