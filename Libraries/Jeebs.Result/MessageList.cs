using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Persists a list of messages in the result chain
	/// </summary>
	public sealed class MessageList
	{
		/// <summary>
		/// The list of messages
		/// </summary>
		private readonly List<IMessage> messages = new List<IMessage>();

		/// <summary>
		/// Clear all messages
		/// </summary>
		public void Clear() => messages.Clear();

		/// <summary>
		/// The number of messages
		/// </summary>
		public int Count { get => messages.Count; }

		/// <summary>
		/// Returns true if <paramref name="m"/> is of type <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">IMessage type</typeparam>
		/// <param name="m">IMessage value</param>
		private bool Match<T>(IMessage m) => typeof(T).IsInstanceOfType(m);

		/// <summary>
		/// Add a single message of type <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">IMessage type</typeparam>
		public void Add<T>() where T : IMessage, new() => messages.Add(new T());

		/// <summary>
		/// Add a single message
		/// </summary>
		/// <typeparam name="T">IMessage type</typeparam>
		/// <param name="message">The message to add</param>
		public void Add<T>(T message) where T : IMessage => messages.Add(message);

		/// <summary>
		/// Add a range of messages
		/// </summary>
		/// <param name="add">Array of messages</param>
		public void AddRange(params IMessage[] add) => add.ToList().ForEach(m => messages.Add(m));

		/// <summary>
		/// Returns whether or not the message list contains at least one message of type <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">IMessage type</typeparam>
		public bool Contains<T>() where T : IMessage => (from m in messages where Match<T>(m) select 1).Any();

		/// <summary>
		/// Get matching messages
		/// </summary>
		/// <typeparam name="T">IMessage type</typeparam>
		public List<T> Get<T>() where T : IMessage => (from m in messages where Match<T>(m) select (T)m).ToList();

		/// <summary>
		/// Get all message values
		/// </summary>
		public List<string> GetAll() => (from m in messages select m.ToString()).ToList();

		/// <summary>
		/// Return all message values on new lines - or default <see cref="ToString"/> if there are no messages
		/// </summary>
		public override string ToString() => messages.Count > 0 ? string.Join('\n', GetAll()) : base.ToString();
	}
}
