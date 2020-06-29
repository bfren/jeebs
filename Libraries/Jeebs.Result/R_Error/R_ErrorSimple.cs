using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult>
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

		/// <inheritdoc/>
		public IError ErrorSimple(params IMessage[] messages) => ErrorSimple(() => Messages.AddRange(messages));

		/// <inheritdoc/>
		public IError ErrorSimple<TMessage>() where TMessage : IMessage, new() => ErrorSimple(() => Messages.Add<TMessage>());

		/// <inheritdoc/>
		public IError ErrorSimple<TMessage>(TMessage message) where TMessage : IMessage => ErrorSimple(() => Messages.Add(message));
	}
}
