using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult, TState>
	{
		/// <summary>
		/// Return this object if <typeparamref name="TNext"/> is actually <typeparamref name="TResult"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="addMessages">Action to add messages</param>
		private IOk<TNext, TState> Ok<TNext>(Action addMessages)
		{
			addMessages();
			return this switch
			{
				IOk<TNext, TState> ok => ok,
				_ => new Ok<TNext, TState>(State) { Messages = Messages }
			};
		}

		#region Keep Result Type

		/// <inheritdoc/>
		public IOk<TResult, TState> Ok(params IMessage[] messages) => Ok<TResult>(() => Messages.AddRange(messages));

		/// <inheritdoc/>
		public IOk<TResult, TState> Ok<TMessage>() where TMessage : IMessage, new() => Ok<TResult>(() => Messages.Add<TMessage>());

		/// <inheritdoc/>
		public IOk<TResult, TState> Ok<TMessage>(TMessage message) where TMessage : IMessage => Ok<TResult>(() => Messages.Add(message));

		#endregion

		#region Change Result Type

		/// <inheritdoc/>
		public IOk<TNext, TState> OkNew<TNext>(params IMessage[] messages) => Ok<TNext>(() => Messages.AddRange(messages));

		/// <inheritdoc/>
		public IOk<TNext, TState> OkNew<TNext, TMessage>() where TMessage : IMessage, new() => Ok<TNext>(() => Messages.Add<TMessage>());

		/// <inheritdoc/>
		public IOk<TNext, TState> OkNew<TNext, TMessage>(TMessage message) where TMessage : IMessage, new() => Ok<TNext>(() => Messages.Add(message));

		#endregion
	}
}
