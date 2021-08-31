// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Unwrap the value of <paramref name="option"/> - if it is <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="ifNone">Value to return if <paramref name="option"/> is a <see cref="Jeebs.Internals.None{T}"/></param>
		public static T Unwrap<T>(Option<T> option, Func<T> ifNone) =>
			Switch(
				option,
				some: v => v,
				none: _ => ifNone()
			);

		/// <summary>
		/// Unwrap the value of <paramref name="option"/> - if it is <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="ifNone">Value to return if <paramref name="option"/> is a <see cref="Jeebs.Internals.None{T}"/></param>
		public static T Unwrap<T>(Option<T> option, Func<IMsg, T> ifNone) =>
			Switch(
				option,
				some: v => v,
				none: ifNone
			);
	}
}
