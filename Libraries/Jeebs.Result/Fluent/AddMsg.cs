// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Fluent
{
	/// <summary>
	/// Fluently add a message by type to the result chain, returning the original <typeparamref name="TResult"/> object
	/// </summary>
	/// <typeparam name="TResult">Result type</typeparam>
	public sealed class AddMsg<TResult>
		where TResult : IR
	{
		private readonly TResult result;

		internal AddMsg(TResult result) =>
			this.result = result;

		/// <summary>
		/// Add a message of type <typeparamref name="TMessage"/> to the result chain and returns <typeparamref name="TResult"/> object
		/// </summary>
		/// <typeparam name="TMessage">Message type</typeparam>
		public TResult OfType<TMessage>()
			where TMessage : IMsg, new() =>
			result.AddMsg(new TMessage());
	}
}
