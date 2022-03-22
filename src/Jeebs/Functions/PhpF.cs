// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Jeebs.Functions;

/// <summary>
/// PHP functions
/// </summary>
public static class PhpF
{
	/// <summary>
	/// Array type
	/// </summary>
	public static readonly char ArrayChar = 'a';

	/// <summary>
	/// Boolean type
	/// </summary>
	public static readonly char BooleanChar = 'b';

	/// <summary>
	/// Double type
	/// </summary>
	public static readonly char DoubleChar = 'd';

	/// <summary>
	/// Integer type
	/// </summary>
	public static readonly char IntegerChar = 'i';

	/// <summary>
	/// String type
	/// </summary>
	public static readonly char StringChar = 's';

	/// <summary>
	/// Null type
	/// </summary>
	public static readonly char NullChar = 'N';

	/// <summary>
	/// UTF8Encoding
	/// </summary>
	private static readonly Encoding UTF8 = new UTF8Encoding();

	/// <summary>
	/// Serialise object
	/// </summary>
	/// <typeparam name="T">Object type</typeparam>
	/// <param name="obj">Object value</param>
	public static string Serialise<T>(T obj) =>
		Serialise(obj, new StringBuilder()).ToString();

	private static StringBuilder Serialise<T>(T obj, StringBuilder sb)
	{
		return obj switch
		{
			string x =>
				appendString(x),

			bool x =>
				append(BooleanChar, x ? "1" : "0"),

			int x =>
				append(IntegerChar, x),

			long x =>
				append(IntegerChar, x),

			float x =>
				append(DoubleChar, x),

			double x =>
				append(DoubleChar, x),

			IList x =>
				appendList(x),

			IDictionary x =>
				appendDictionary(x),

			{ } x =>
				sb,

			_ =>
				sb.Append(NullChar).Append(';')
		};

		// Append a value to the StringBuilder
		StringBuilder append<TValue>(char type, TValue value) =>
			sb.Append(CultureInfo.InvariantCulture, $"{type}:{value};");

		// Append a string to the StringBuilder
		StringBuilder appendString(string str) =>
			sb.Append(CultureInfo.InvariantCulture, $"{StringChar}:{UTF8.GetByteCount(str)}:\"{str}\";");

		// Append a Hashtable to the StringBuilder
		// Enables arrays of different key / value pairs
		StringBuilder appendHashtable(Hashtable hashtable)
		{
			_ = sb.Append(CultureInfo.InvariantCulture, $"{ArrayChar}:{hashtable.Count}:{{");
			foreach (DictionaryEntry item in hashtable)
			{
				_ = Serialise(item.Key, sb);
				_ = Serialise(item.Value, sb);
			}
			return sb.Append('}');
		}

		// Append a List to the StringBuilder
		StringBuilder appendList(IList list)
		{
			var htb = new Hashtable();
			for (var i = 0; i < list.Count; i++)
			{
				htb.Add(i, list[i]);
			}

			return appendHashtable(htb);
		}

		// Append a Dictionary to the StringBuilder
		StringBuilder appendDictionary(IDictionary d) =>
			appendHashtable(new Hashtable(d));
	}

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

	/// <summary>
	/// Alias for use in deserialising associative arrays
	/// </summary>
	public sealed class AssocArray : Dictionary<object, object> { }
}
