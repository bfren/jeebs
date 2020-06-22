using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<T>
	{
		/// <summary>
		/// Return this object if <typeparamref name="TNext"/> is actually <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="addMessages">Action to add messages</param>
		private Ok<TNext> Ok<TNext>(Action addMessages)
		{
			addMessages();
			return this switch
			{
				Ok<TNext> ok => ok,
				_ => new Ok<TNext> { Messages = Messages }
			};
		}

		/// <summary>
		/// Return new object of type <see cref="Jeebs.OkV{T}"/> with value <paramref name="value"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="addMessages">Action to add messages</param>
		private OkV<TNext> OkV<TNext>(TNext value, Action addMessages)
		{
			addMessages();
			return new OkV<TNext>(value) { Messages = Messages };
		}

		#region Keep Result Type

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public Ok<T> Ok(params IMessage[] messages) => Ok<T>(() => Messages.AddRange(messages));

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Ok<T> Ok<TMessage>() where TMessage : IMessage, new() => Ok<T>(() => Messages.Add<TMessage>());

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Ok<T> Ok<TMessage>(TMessage message) where TMessage : IMessage => Ok<T>(() => Messages.Add(message));

		#endregion

		#region Change Result Type

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="messages">IMessage array</param>
		public Ok<TNext> OkNew<TNext>(params IMessage[] messages) => Ok<TNext>(() => Messages.AddRange(messages));

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Ok<TNext> OkNew<TNext, TMessage>() where TMessage : IMessage, new() => Ok<TNext>(() => Messages.Add<TMessage>());

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="message">Message value</param>
		public Ok<TNext> OkNew<TNext, TMessage>(TMessage message) where TMessage : IMessage, new() => Ok<TNext>(() => Messages.Add(message));

		#endregion

		#region Change Result Type and Add Value

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="messages">IMessage array</param>
		public OkV<TNext> OkV<TNext>(TNext value, params IMessage[] messages) => OkV(value, () => Messages.AddRange(messages));

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="value">Ok result value</param>
		public OkV<TNext> OkV<TNext, TMessage>(TNext value) where TMessage : IMessage, new() => OkV(value, () => Messages.Add<TMessage>());

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="message">Message value</param>
		public OkV<TNext> OkV<TNext, TMessage>(TNext value, TMessage message) where TMessage : IMessage, new() => OkV(value, () => Messages.Add(message));

		#endregion
	}
}
