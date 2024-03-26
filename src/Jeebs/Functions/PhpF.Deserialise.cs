// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Functions;

public static partial class PhpF
{
	/// <summary>
	/// Deserialise object.
	/// </summary>
	/// <param name="str">Serialised string.</param>
	public static object Deserialise(string str)
	{
		var pointer = 0;
		return PrivateDeserialise(str, ref pointer);
	}

	private static object PrivateDeserialise(string str, ref int pointer)
	{
		if (string.IsNullOrWhiteSpace(str) || str.Length <= pointer)
		{
			return new object();
		}

		return str[pointer] switch
		{
			char c when c == ArrayChar =>
				getArray(str, ref pointer),

			char c when c == BooleanChar =>
				getBoolean(str, ref pointer),

			char c when c == DoubleChar =>
				getNumber(str, double.Parse, 0d, ref pointer),

			char c when c == IntegerChar =>
				getNumber(str, long.Parse, 0L, ref pointer),

			char c when c == StringChar =>
				getString(str, ref pointer),

			char c when c == NullChar =>
				getNull(ref pointer),

			_ =>
				string.Empty
		};

		// Get null object
		static object getNull(ref int pointer)
		{
			pointer += 2;
			return string.Empty;
		}

		// Get boolean
		static bool getBoolean(string str, ref int pointer)
		{
			var b = str[pointer + 2];
			pointer += 4;
			return b == '1';
		}

		// Get a number (long or double)
		static T getNumber<T>(string str, Func<string, T> parse, T ifError, ref int pointer)
		{
			// Get string value
			var colon = str.IndexOf(':', pointer) + 1;
			var semicolon = str.IndexOf(';', colon);
			var num = str[colon..semicolon]; // the number as a string
			pointer += 3 + num.Length;

			// Attempt to parse number value
			try
			{
				return parse(num);
			}
			catch (Exception)
			{
				return ifError;
			}
		}

		// Get string
		static string getString(string str, ref int pointer)
		{
			// Get start and end positions
			var colon0 = str.IndexOf(':', pointer) + 1;
			var colon1 = str.IndexOf(':', colon0);
			var semicolon = str.IndexOf(';', pointer);
			pointer = semicolon + 1;

			// Calculate length
			var from = colon1 + 2; // start two characters after the second colon
			var to = semicolon - 1; // end one character before the semicolon

			// Get substring
			return str[from..to];
		}

		// Get AssocArray
		static AssocArray getArray(string str, ref int pointer)
		{
			// Get start and end positions
			var colon0 = str.IndexOf(':', pointer) + 1;
			var colon1 = str.IndexOf(':', colon0);
			var num = str[colon0..colon1]; // the number of items in the array
			var len = int.Parse(num, CultureInfo.InvariantCulture);
			pointer += 4 + num.Length;

			// Get each key and value, and add them to a hashtable
			var table = new AssocArray();
			for (var i = 0; i < len; i++)
			{
				var (key, value) = (PrivateDeserialise(str, ref pointer), PrivateDeserialise(str, ref pointer));
				M.ParseInt32(key.ToString()).Match(
					some: x => table.Add(x, value),
					none: () => table.Add(key, value)
				);
			}

			pointer++;
			return table;
		}
	}
}
