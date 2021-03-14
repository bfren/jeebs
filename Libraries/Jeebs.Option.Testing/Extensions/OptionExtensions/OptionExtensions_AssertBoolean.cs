// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs
{
	/// <summary>
	/// Option Extensions: AssertBoolean
	/// </summary>
	public static class OptionExtensions_AssertBoolean
	{
		/// <summary>
		/// Assert that <paramref name="this"/> is <see cref="Some{T}"/> and the value is false
		/// </summary>
		/// <param name="this"></param>
		public static void AssertFalse(this Option<bool> @this) =>
			Assert.False(@this.AssertSome());

		/// <summary>
		/// Assert that <paramref name="this"/> is <see cref="Some{T}"/> and the value is true
		/// </summary>
		/// <param name="this"></param>
		public static void AssertTrue(this Option<bool> @this) =>
			Assert.True(@this.AssertSome());
	}
}
