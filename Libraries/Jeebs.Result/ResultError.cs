using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<T>
	{
		#region Keep Result Type

		/// <summary>
		/// Return Error result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public Error<T> Error(params IMessage[] messages)
		{
			Messages.AddRange(messages);
			return SkipAhead();
		}

		/// <summary>
		/// Return Error result with a single message
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Error<T> Error<TMessage>() where TMessage : IMessage, new()
		{
			Messages.Add<TMessage>();
			return SkipAhead();
		}

		/// <summary>
		/// Return Error result with a single message
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Error<T> Error<TMessage>(TMessage message) where TMessage : IMessage
		{
			Messages.Add(message);
			return SkipAhead();
		}

		#endregion

		#region Change Result Type

		/// <summary>
		/// Return Error result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="messages">IMessage array</param>
		public Error<TNext> Error<TNext>(params IMessage[] messages)
		{
			Messages.AddRange(messages);
			return SkipAhead<TNext>();
		}

		/// <summary>
		/// Return Error result with a single message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public Error<TNext> Error<TNext, TMessage>() where TMessage : IMessage, new()
		{
			Messages.Add<TMessage>();
			return SkipAhead<TNext>();
		}

		/// <summary>
		/// Return Error result with a single message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="message">Message value</param>
		public Error<TNext> Error<TNext, TMessage>(TMessage message) where TMessage : IMessage, new()
		{
			Messages.Add(message);
			return SkipAhead<TNext>();
		}

		#endregion

		#region Simple

		/// <summary>
		/// Return Simple Error result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public Error ErrorSimple(params IMessage[] messages)
		{
			Messages.AddRange(messages);
			return new Error { Messages = Messages };
		}

		#endregion
	}
}
