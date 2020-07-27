using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Jeebs.Result.Fluent
{
	/// <summary>
	/// Fluent with allows adding message(s) to the result chain, returning the <typeparamref name="TResult"/> object
	/// </summary>
	/// <typeparam name="TResult">Result type</typeparam>
	public sealed class With<TResult>
		where TResult : IR
	{
		private readonly TResult result;

		internal With(TResult result) => this.result = result;

		/// <summary>
		/// Add a message of type <typeparamref name="TMessage"/> to the result chain and returns <typeparamref name="TResult"/> object
		/// </summary>
		/// <typeparam name="TMessage">Message type</typeparam>
		public TResult Message<TMessage>()
			where TMessage : IMsg, new()
		{
			result.Messages.Add<TMessage>();
			return result;
		}

		/// <summary>
		/// Add a message of type <typeparamref name="TMessage"/> to the result chain and returns <typeparamref name="TResult"/> object
		/// </summary>
		/// <typeparam name="TMessage">Message type</typeparam>
		/// <param name="message">Message</param>
		public TResult Message<TMessage>(TMessage message)
			where TMessage : IMsg
		{
			result.Messages.Add(message);
			return result;
		}

		/// <summary>
		/// Adds message(s) to the result chain and returns <typeparamref name="TResult"/> object
		/// </summary>
		/// <param name="messages">Messages to add - must be at least one</param>
		public TResult Messages([NotNull] params IMsg[] messages)
		{
			result.Messages.AddRange(messages);
			return result;
		}
	}
}
