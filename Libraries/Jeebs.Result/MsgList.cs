// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs
{
	/// <inheritdoc cref="IMsgList"/>
	public class MsgList : IMsgList
	{
		/// <summary>
		/// The list of messages
		/// </summary>
		private readonly List<IMsg> messages = new();

		/// <inheritdoc/>
		public int Count =>
			messages.Count;

		/// <inheritdoc/>
		public void Clear() =>
			messages.Clear();

		/// <summary>
		/// Returns true if <paramref name="m"/> is of type <typeparamref name="TMsg"/>
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		/// <param name="m">IMsg value</param>
		private static bool Match<TMsg>(IMsg m) =>
			typeof(TMsg).IsInstanceOfType(m);

		/// <inheritdoc/>
		public void Add<TMsg>() where TMsg : IMsg, new() =>
			messages.Add(new TMsg());
		/// <inheritdoc/>
		public void Add<TMsg>(TMsg message) where TMsg : IMsg =>
			messages.Add(message);

		/// <inheritdoc/>
		public void AddRange(params IMsg[] add) =>
			add.ToList().ForEach(m => messages.Add(m));

		/// <inheritdoc/>
		public bool Contains<TMsg>() where TMsg : IMsg =>
			(from m in messages where Match<TMsg>(m) select 1).Any();

		/// <inheritdoc/>
		public List<TMsg> Get<TMsg>() where TMsg : IMsg =>
			(from m in messages where Match<TMsg>(m) select (TMsg)m).ToList();

		/// <inheritdoc/>
		public List<string> GetAll(bool withType = false) =>
			withType switch
			{
				true =>
					(from m in messages select $"{m.GetType()}: {m}").ToList(),

				false =>
					(from m in messages select m.ToString()).ToList()
			};

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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
