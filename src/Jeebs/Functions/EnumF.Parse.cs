// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Functions;

public static partial class EnumF
{
	/// <summary>
	/// Parse a string value into the specified Enum.
	/// </summary>
	/// <typeparam name="T">Enum type</typeparam>
	/// <param name="value">The value to parse.</param>
	public static Result<T> Parse<T>(string? value)
		where T : struct, Enum
	{
		if (value is null)
		{
			return FailNullValue<T>();
		}

		try
		{
			return Enum.Parse<T>(value);
		}
		catch (Exception)
		{
			return FailNotAValidEnumValue<T>(value);
		}
	}
}
