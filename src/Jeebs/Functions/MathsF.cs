// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace F
{
	/// <summary>
	/// Maths Functions
	/// </summary>
	public static class MathsF
	{
		/// <summary>
		/// Calculate the factorial result of <paramref name="x"/>
		/// </summary>
		/// <remarks>
		/// <paramref name="x"/> must be greater than or equal to zero
		/// </remarks>
		/// <param name="x">Number (greater than or equal to zero)</param>
		public static long Factorial(long x)
		{
			if (x < 0)
			{
				return -1;
			}

			var result = 1L;
			for (long i = x; i > 1; i--)
			{
				result *= i;
			}

			return result;
		}

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

		/// <summary>
		/// Calculate the number of permutations - i.e. the number of ways <paramref name="r"/> objects can be arranged
		/// among <paramref name="n"/> places<br/>
		/// Use this when <strong>order matters</strong>
		/// </summary>
		/// <remarks>
		/// <paramref name="n"/> and <paramref name="r"/> must be greater than 0,
		/// and <paramref name="n"/> must be greater than or equal to <paramref name="r"/>
		/// </remarks>
		/// <param name="n">The total number of objects</param>
		/// <param name="r">The number of places being filled</param>
		public static long Permutations(long n, long r) =>
			(n > 0 && r > 0 && n >= r) switch
			{
				true =>
					Factorial(n) / Factorial(n - r),

				false =>
					-1
			};
	}
}
