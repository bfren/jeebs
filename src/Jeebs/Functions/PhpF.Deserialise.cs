// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Functions;

public static partial class PhpF
{
	private static int pointer;

	/// <summary>
	/// Deserialise object
	/// </summary>
	/// <param name="str">Serialised string</param>
	public static object Deserialise(string str)
	{
		pointer = 0;
		return PrivateDeserialise(str);
	}

	private static object PrivateDeserialise(string str)
	{
		if (string.IsNullOrWhiteSpace(str) || str.Length <= pointer)
		{
			return new object();
		}

		return str[pointer] switch
		{
			char c when c == ArrayChar =>
				getArray(),

			char c when c == BooleanChar =>
				getBoolean(),

			char c when c == DoubleChar =>
				getNumber(double.Parse, 0d),

			char c when c == IntegerChar =>
				getNumber(long.Parse, 0L),

			char c when c == StringChar =>
				getString(),

			char c when c == NullChar =>
				getNull(),

			_ =>
				string.Empty
		};

		// Get null object
		static object getNull()
		{
			pointer += 2;
			return string.Empty;
		}

		// Get boolean
		bool getBoolean()
		{
			var b = str[pointer + 2];
			pointer += 4;
			return b == '1';
		}

		// Get a number (long or double)
		T getNumber<T>(Func<string, T> parse, T ifError)
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
		string getString()
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
		AssocArray getArray()
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
				var (key, value) = (PrivateDeserialise(str), PrivateDeserialise(str));
				F.ParseInt32(key.ToString()).Switch(
					some: x => table.Add(x, value),
					none: _ => table.Add(key, value)
				);
			}

			pointer++;
			return table;
		}
	}
}
