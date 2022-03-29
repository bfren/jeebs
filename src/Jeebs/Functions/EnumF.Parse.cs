// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Functions;

public static partial class EnumF
{
	/// <summary>
	/// Parse a string value into the specified Enum
	/// </summary>
	/// <typeparam name="T">Enum type</typeparam>
	/// <param name="value">The value to parse</param>
	public static Maybe<T> Parse<T>(string value)
		where T : struct, Enum
	{
		try
		{
			return Enum.Parse(typeof(T), value) switch
			{
				T x =>
					x,

				_ =>
					F.None<T>(new M.NotAValidEnumValueMsg<T>(value))
			};
		}
		catch (Exception)
		{
			return F.None<T>(new M.NotAValidEnumValueMsg<T>(value));
		}
	}

	/// <summary>
	/// Parse a string value into the specified Enum
	/// </summary>
	/// <param name="t">Enum type</param>
	/// <param name="value">The value to parse</param>
	public static Maybe<object> Parse(Type t, string value)
	{
		if (!t.IsEnum)
		{
			return F.None<object>(new M.NotAValidEnumMsg(t));
		}

		try
		{
			return Enum.Parse(t, value, true);
		}
		catch (Exception)
		{
			return F.None<object>(new M.NotAValidEnumValueMsg(t, value));
		}
	}
}
