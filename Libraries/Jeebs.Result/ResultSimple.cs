using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<T>
	{
		#region Ok

		/// <summary>
		/// Return this object if it is already an <see cref="Jeebs.Ok"/>, otherwise create a new one
		/// </summary>
		/// <param name="addMessages">Action to add messages</param>
		private Ok OkSimple(Action addMessages)
		{
			addMessages();
			return this switch
			{
				Ok ok => ok,
				_ => new Ok { Messages = Messages }
			};
		}

		/// <summary>
		/// Return Simple Ok result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public Ok OkSimple(params IMessage[] messages) => OkSimple(() => Messages.AddRange(messages));

		/// <summary>
		/// Return Simple Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Ok OkSimple<TMessage>() where TMessage : IMessage, new() => OkSimple(() => Messages.Add<TMessage>());

		/// <summary>
		/// Return Simple Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Ok OkSimple<TMessage>(TMessage message) where TMessage : IMessage => OkSimple(() => Messages.Add(message));

		#endregion

		#region Error

		/// <summary>
		/// Return this object if it is already an <see cref="Jeebs.Error"/>, otherwise create a new one
		/// </summary>
		/// <param name="addMessages">Action to add messages</param>
		private Error ErrorSimple(Action addMessages)
		{
			addMessages();
			return this switch
			{
				Error error => error,
				_ => new Error { Messages = Messages }
			};
		}

		/// <summary>
		/// Return Simple Error result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public Error ErrorSimple(params IMessage[] messages) => ErrorSimple(() => Messages.AddRange(messages));

		/// <summary>
		/// Return Simple Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Error ErrorSimple<TMessage>() where TMessage : IMessage, new() => ErrorSimple(() => Messages.Add<TMessage>());

		/// <summary>
		/// Return Simple Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Error ErrorSimple<TMessage>(TMessage message) where TMessage : IMessage => ErrorSimple(() => Messages.Add(message));

		#endregion
	}
}
