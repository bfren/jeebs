// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Fluent
{
	/// <summary>
	/// Add custom exception handling to the chain
	/// </summary>
	/// <typeparam name="TValue">Result / Link value type</typeparam>
	/// <typeparam name="TState">Result / Link state type</typeparam>
	public sealed class Catch<TValue, TState>
	{
		private readonly ILink<TValue, TState> link;

		internal Catch(ILink<TValue, TState> link) =>
			this.link = link;

		/// <inheritdoc cref="Catch{TValue}.AllUnhandled"/>
		public CatchException<TValue, TState, Exception> AllUnhandled() =>
			new(link);

		/// <inheritdoc cref="Catch{TValue}.Unhandled{TException}"/>
		public CatchException<TValue, TState, TException> Unhandled<TException>()
			where TException : Exception =>
			new(link);
	}
}
