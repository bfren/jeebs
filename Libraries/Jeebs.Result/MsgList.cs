using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Persists a list of messages (of type <see cref="IMsg"/>) in the result chain
	/// </summary>
	public class MsgList : IDisposable
	{
		/// <summary>
		/// The list of messages
		/// </summary>
		private readonly List<IMsg> messages = new List<IMsg>();

		/// <summary>
		/// The number of messages
		/// </summary>
		public int Count =>
			messages.Count;

		/// <summary>
		/// Clear all messages
		/// </summary>
		public void Clear() =>
			messages.Clear();

		/// <summary>
		/// Dispose (<seealso cref="Clear"/>)
		/// </summary>
		public void Dispose()
		{
			Clear();
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Returns true if <paramref name="m"/> is of type <typeparamref name="TMsg"/>
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		/// <param name="m">IMsg value</param>
		private static bool Match<TMsg>(IMsg m) =>
			typeof(TMsg).IsInstanceOfType(m);

		/// <summary>
		/// Add a single message of type <typeparamref name="TMsg"/>
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		internal void Add<TMsg>() where TMsg : IMsg, new() =>
			messages.Add(new TMsg());

		/// <summary>
		/// Add a single message
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		/// <param name="message">The message to add</param>
		internal void Add<TMsg>(TMsg message) where TMsg : IMsg =>
			messages.Add(message);

		/// <summary>
		/// Add a range of messages
		/// </summary>
		/// <param name="add">Array of messages</param>
		internal void AddRange(params IMsg[] add) =>
			add.ToList().ForEach(m => messages.Add(m));

		/// <summary>
		/// Returns whether or not the message list contains at least one message of type <typeparamref name="TMsg"/>
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		public bool Contains<TMsg>() where TMsg : IMsg =>
			(from m in messages where Match<TMsg>(m) select 1).Any();

		/// <summary>
		/// Get matching messages
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		public List<TMsg> Get<TMsg>() where TMsg : IMsg =>
			(from m in messages where Match<TMsg>(m) select (TMsg)m).ToList();

		/// <summary>
		/// Get all message values
		/// </summary>
		/// <param name="withType">[Optional] If true, will include the message type as well</param>
		public List<string> GetAll(bool withType = false) =>
			withType switch
			{
				true =>
					(from m in messages select $"{m.GetType()}: {m}").ToList(),

				false =>
					(from m in messages select m.ToString()).ToList()
			};

		/// <summary>
		/// Returns the messages list as an Enumerable
		/// </summary>
		public IEnumerable<IMsg> GetEnumerable()
		{
			foreach (var m in messages)
			{
				yield return m;
			}
		}

		/// <summary>
		/// Return all message values on new lines - or default <see cref="ToString()"/> if there are no messages
		/// </summary>
		public override string ToString() =>
			ToString(false);

		/// <summary>
		/// Return all message values on new lines - or default <see cref="ToString(bool)"/> if there are no messages
		/// </summary>
		/// <param name="withType">If true, will include the message type as well</param>
		public string ToString(bool withType) =>
			messages.Count switch
			{
				> 0 =>
					string.Join('\n', GetAll(withType)),

				_ =>
					GetType().ToString()
			};
	}
}
