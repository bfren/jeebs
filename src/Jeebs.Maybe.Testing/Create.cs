// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Create objects for testing
/// </summary>
public static class Create
{
	/// <summary>
	/// Create an empty <see cref="Internals.None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	public static Maybe<T> None<T>() =>
		F.MaybeF.None<T, M.EmptyNoneForTestingMsg>();

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Empty None created for testing</summary>
		public sealed record class EmptyNoneForTestingMsg : Msg;
	}
}
