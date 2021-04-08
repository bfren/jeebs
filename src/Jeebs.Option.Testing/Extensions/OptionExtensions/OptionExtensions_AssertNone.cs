// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs
{
	/// <summary>
	/// Option Extensions: AssertNone
	/// </summary>
	public static class OptionExtensions_AssertNone
	{
		/// <summary>
		/// Assert that <paramref name="this"/> is <see cref="None{T}"/> and return the Reason
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option</param>
		public static IMsg AssertNone<T>(this Option<T> @this) =>
			Assert.IsType<None<T>>(@this).Reason;
	}
}
