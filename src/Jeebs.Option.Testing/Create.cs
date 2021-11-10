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
	/// <typeparam name="T">Option value type</typeparam>
	public static Option<T> None<T>() =>
		F.OptionF.None<T, Msg.EmptyNoneForTestingMsg>();

	/// <summary>Messages</summary>
	public static class Msg
	{
		/// <summary>Empty None created for testing</summary>
		public sealed record class EmptyNoneForTestingMsg : IMsg { };
	}
}
