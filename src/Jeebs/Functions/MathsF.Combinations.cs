// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions;

public static partial class MathsF
{
	/// <summary>
	/// Calculate the number of combinations - i.e. the number of ways to choose or combine <paramref name="r"/> objects
	/// from a total of <paramref name="n"/> objects<br/>
	/// Use this when <strong>order does not matter</strong>
	/// </summary>
	/// <remarks>
	/// <paramref name="n"/> and <paramref name="r"/> must be greater than 0,
	/// and <paramref name="n"/> must be greater than or equal to <paramref name="r"/>
	/// </remarks>
	/// <param name="n">The total number of objects</param>
	/// <param name="r">The number of objects being selected</param>
	public static long Combinations(long n, long r) =>
		(n > 0 && r > 0 && n >= r) switch
		{
			true =>
				Factorial(n) / (Factorial(r) * Factorial(n - r)),

			false =>
				-1
		};
}
