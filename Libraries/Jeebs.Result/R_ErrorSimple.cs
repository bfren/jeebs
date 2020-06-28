using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<T>
	{
		/// <summary>
		/// Return this object if it is already an <see cref="Jeebs.Error"/>, otherwise create a new one
		/// </summary>
		/// <param name="addMessages">Action to add messages</param>
		private IError ErrorSimple(Action addMessages)
		{
			addMessages();
			return this switch
			{
				IError error => error,
				_ => new Error { Messages = Messages }
			};
		}

		/// <summary>
		/// Return Simple Error result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public IError ErrorSimple(params IMessage[] messages) => ErrorSimple(() => Messages.AddRange(messages));

		/// <summary>
		/// Return Simple Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IError ErrorSimple<TMessage>() where TMessage : IMessage, new() => ErrorSimple(() => Messages.Add<TMessage>());

		/// <summary>
		/// Return Simple Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IError ErrorSimple<TMessage>(TMessage message) where TMessage : IMessage => ErrorSimple(() => Messages.Add(message));
	}
}
