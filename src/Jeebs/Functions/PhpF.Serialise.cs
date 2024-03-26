// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections;
using System.Globalization;
using System.Text;

namespace Jeebs.Functions;

public static partial class PhpF
{
	/// <summary>
	/// Serialise object.
	/// </summary>
	/// <typeparam name="T">Object type.</typeparam>
	/// <param name="obj">Object value.</param>
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
}
