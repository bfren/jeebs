// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Internals;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Create a <see cref="Jeebs.Internals.None{T}"/> Option with a Reason message
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="reason">Reason message</param>
		public static None<T> None<T>(IMsg reason) =>
			new(reason);

		/// <summary>
		/// Create a <see cref="Jeebs.Internals.None{T}"/> Option with a Reason message by type
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="TMsg">Reason message type</typeparam>
		public static None<T> None<T, TMsg>()
			where TMsg : IMsg, new() =>
			new(new TMsg());

		/// <summary>
		/// Create a <see cref="Jeebs.Internals.None{T}"/> Option with a Reason exception message by type
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="TExceptionMsg">Reason exception message type</typeparam>
		/// <param name="ex">Exception object</param>
		public static None<T> None<T, TExceptionMsg>(Exception ex)
			where TExceptionMsg : IExceptionMsg, new() =>
			new(new TExceptionMsg() { Exception = ex });
	}
}
