﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs;
using static F.OptionF;

namespace F;

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
			return None<bool, M.NullValueMsg>();
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

		return None<bool>(new M.UnrecognisedValueMsg(val));
	}

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Null Value</summary>
		public sealed record class NullValueMsg : Msg;

		/// <summary>Unrecognised boolean value</summary>
		/// <param name="Value">Unrecognised Value</param>
		public sealed record class UnrecognisedValueMsg(string Value) : WithValueMsg<string>() { }
	}
}
