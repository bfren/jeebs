// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Internals;
using Xunit;

namespace Jeebs
{
	/// <summary>
	/// Option Extensions: AssertSome
	/// </summary>
	public static class OptionExtensions_AssertSome
	{
		/// <summary>
		/// Assert that <paramref name="this"/> is <see cref="Some{T}"/> and return the wrapped value
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option</param>
		public static T AssertSome<T>(this Option<T> @this) =>
			Assert.IsType<Some<T>>(@this).Value;
	}
}
