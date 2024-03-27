// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Functions;

public static partial class EnumF
{
	/// <summary>
	/// Parse a string value into the specified Enum.
	/// </summary>
	/// <typeparam name="T">Enum type.</typeparam>
	/// <param name="value">The value to parse.</param>
	/// <returns><paramref name="value"/> parsed as a <typeparamref name="T"/> Enum value.</returns>
	public static Maybe<T> Parse<T>(string? value)
		where T : struct, Enum
	{
		if (value is null)
		{
			return M.None;
		}

		try
		{
			return Enum.Parse(typeof(T), value) switch
			{
				T x =>
					x,

				_ =>
					M.None
			};
		}
		catch (Exception)
		{
			return M.None;
		}
	}

	/// <summary>
	/// Parse a string value into the specified Enum.
	/// </summary>
	/// <param name="t">Enum type.</param>
	/// <param name="value">The value to parse.</param>
	/// <returns><paramref name="value"/> parsed as an Enum value of type <paramref name="t"/>.</returns>
	public static Maybe<object> Parse(Type t, string? value)
	{
		if (!t.IsEnum)
		{
			return M.None;
		}

		if (value is null)
		{
			return M.None;
		}

		try
		{
			return Enum.Parse(t, value, true);
		}
		catch (Exception)
		{
			return M.None;
		}
	}
}
