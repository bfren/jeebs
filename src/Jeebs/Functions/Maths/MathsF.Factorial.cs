// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions;

public static partial class MathsF
{
	/// <summary>
	/// Calculate the factorial result of <paramref name="x"/>.
	/// </summary>
	/// <remarks>
	/// <paramref name="x"/> must be greater than or equal to zero.
	/// </remarks>
	/// <param name="x">Number (greater than or equal to zero).</param>
	/// <returns>Factorial result.</returns>
	public static long Factorial(long x)
	{
		if (x < 0)
		{
			return -1;
		}

		var result = 1L;
		for (var i = x; i > 1; i--)
		{
			result *= i;
		}

		return result;
	}
}
