using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult>
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

		/// <inheritdoc/>
		public IOk OkSimple(params IMessage[] messages) => OkSimple(() => Messages.AddRange(messages));

		/// <inheritdoc/>
		public IOk OkSimple<TMessage>() where TMessage : IMessage, new() => OkSimple(() => Messages.Add<TMessage>());

		/// <inheritdoc/>
		public IOk OkSimple<TMessage>(TMessage message) where TMessage : IMessage => OkSimple(() => Messages.Add(message));
	}
}
