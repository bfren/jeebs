using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<T>
	{
		/// <summary>
		/// Return this object if it is already an <see cref="Jeebs.Ok"/>, otherwise create a new one
		/// </summary>
		/// <param name="addMessages">Action to add messages</param>
		private IOk OkSimple(Action addMessages)
		{
			addMessages();
			return this switch
			{
				IOk ok => ok,
				_ => new Ok { Messages = Messages }
			};
		}

		/// <summary>
		/// Return Simple Ok result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public IOk OkSimple(params IMessage[] messages) => OkSimple(() => Messages.AddRange(messages));

		/// <summary>
		/// Return Simple Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IOk OkSimple<TMessage>() where TMessage : IMessage, new() => OkSimple(() => Messages.Add<TMessage>());

		/// <summary>
		/// Return Simple Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IOk OkSimple<TMessage>(TMessage message) where TMessage : IMessage => OkSimple(() => Messages.Add(message));
	}
}
