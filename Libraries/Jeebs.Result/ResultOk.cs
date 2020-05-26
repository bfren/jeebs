using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<T>
	{
		#region Keep Result Type

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public Ok<T> Ok(params IMessage[] messages)
		{
			Messages.AddRange(messages);
			return (Ok<T>)this;
		}

		/// <summary>
		/// Return Ok result with a single message
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Ok<T> Ok<TMessage>() where TMessage : IMessage, new()
		{
			Messages.Add<TMessage>();
			return (Ok<T>)this;
		}

		/// <summary>
		/// Return Ok result with a single message
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Ok<T> Ok<TMessage>(TMessage message) where TMessage : IMessage
		{
			Messages.Add(message);
			return (Ok<T>)this;
		}

		#endregion

		#region Change Result Type

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="messages">IMessage array</param>
		public Ok<TNext> Ok<TNext>(params IMessage[] messages)
		{
			Messages.AddRange(messages);
			return new Ok<TNext> { Messages = Messages };
		}

		/// <summary>
		/// Return Ok result with a single message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Ok<TNext> Ok<TNext, TMessage>() where TMessage : IMessage, new()
		{
			Messages.Add<TMessage>();
			return new Ok<TNext> { Messages = Messages };
		}

		/// <summary>
		/// Return Ok result with a single message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="message">Message value</param>
		public Ok<TNext> Ok<TNext, TMessage>(TMessage message) where TMessage : IMessage, new()
		{
			Messages.Add(message);
			return new Ok<TNext> { Messages = Messages };
		}

		#endregion

		#region Change Result Type and Add Value

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="messages">IMessage array</param>
		public OkV<TNext> OkV<TNext>(TNext value, params IMessage[] messages)
		{
			Messages.AddRange(messages);
			return new OkV<TNext>(value) { Messages = Messages };
		}

		/// <summary>
		/// Return Ok result with a single message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="value">Ok result value</param>
		public OkV<TNext> OkV<TNext, TMessage>(TNext value) where TMessage : IMessage, new()
		{
			Messages.Add<TMessage>();
			return new OkV<TNext>(value) { Messages = Messages };
		}

		/// <summary>
		/// Return Ok result with a single message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="message">Message value</param>
		public OkV<TNext> OkV<TNext, TMessage>(TNext value, TMessage message) where TMessage : IMessage, new()
		{
			Messages.Add(message);
			return new OkV<TNext>(value) { Messages = Messages };
		}

		#endregion

		#region Simple

		/// <summary>
		/// Return Simple Ok result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public Ok OkSimple(params IMessage[] messages)
		{
			Messages.AddRange(messages);
			return new Ok { Messages = Messages };
		}

		#endregion
	}
}
