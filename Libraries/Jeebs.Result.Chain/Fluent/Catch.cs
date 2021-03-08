// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Fluent
{
	/// <summary>
	/// Add custom exception handling to the chain
	/// </summary>
	/// <typeparam name="TValue">Result / Link value type</typeparam>
	public sealed class Catch<TValue>
	{
		private readonly ILink<TValue> link;

		internal Catch(ILink<TValue> link) =>
			this.link = link;

		/// <summary>
		/// Catch all unhandled exceptions
		/// </summary>
		public CatchException<TValue, Exception> AllUnhandled() =>
			new(link);

		/// <summary>
		/// Catch unhandled exceptions of a certain type
		/// </summary>
		/// <typeparam name="TException">Exception type to handle</typeparam>
		public CatchException<TValue, TException> Unhandled<TException>()
			where TException : Exception =>
			new(link);
	}
}
