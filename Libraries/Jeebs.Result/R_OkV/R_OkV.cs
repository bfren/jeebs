using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult>
	{
		/// <summary>
		/// Return new object of type <see cref="Jeebs.OkV{T}"/> with value <paramref name="value"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="addMessages">Action to add messages</param>
		private IOkV<TNext> OkV<TNext>(TNext value, Action addMessages)
		{
			addMessages();
			return new OkV<TNext>(value) { Messages = Messages };
		}

		/// <inheritdoc/>
		public IOkV<TNext> OkV<TNext>(TNext value, params IMessage[] messages) => OkV(value, () => Messages.AddRange(messages));

		/// <inheritdoc/>
		public IOkV<TNext> OkV<TNext, TMessage>(TNext value) where TMessage : IMessage, new() => OkV(value, () => Messages.Add<TMessage>());

		/// <inheritdoc/>
		public IOkV<TNext> OkV<TNext, TMessage>(TNext value, TMessage message) where TMessage : IMessage, new() => OkV(value, () => Messages.Add(message));

	}
}
