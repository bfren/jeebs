// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions;

public static partial class MathsF
{
	/// <summary>
	/// Calculate the number of permutations - i.e. the number of ways <paramref name="r"/> objects can be arranged
	/// among <paramref name="n"/> placesbr/>
	/// Use this when <strong>order matters</strong>.
	/// </summary>
	/// <remarks>
	/// <paramref name="n"/> and <paramref name="r"/> must be greater than 0,
	/// and <paramref name="n"/> must be greater than or equal to <paramref name="r"/>.
	/// </remarks>
	/// <param name="n">The total number of objects.</param>
	/// <param name="r">The number of places being filled.</param>
	public static long Permutations(long n, long r) =>
		(n > 0 && r > 0 && n >= r) switch
		{
			true =>
				Factorial(n) / Factorial(n - r),

			false =>
				-1
		};
}
