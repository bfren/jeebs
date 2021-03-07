// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Fluent;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/>: ChangeType
	/// </summary>
	public static class RExtensions_ChangeType
	{
		/// <summary>
		/// Allows the changing of a result value type
		/// <para><see cref="IOk"/> and <see cref="IError"/> stay the same with the new type</para>
		/// <para>However, if <paramref name="this"/> is an <see cref="IOkV{TValue}"/> the <see cref="IOkV{TValue}.Value"/> will be lost.</para>
		/// </summary>
		/// <param name="this">Result</param>
		public static ChangeType ChangeType(this IR @this) =>
			new(@this);

		/// <inheritdoc cref="ChangeType(IR)"/>
		public static ChangeType<TValue, TState> ChangeType<TValue, TState>(this IR<TValue, TState> @this) =>
			new(@this);
	}
}
