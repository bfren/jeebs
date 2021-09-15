// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs;

namespace F;

public static partial class OptionF
{
	public static partial class Dictionary
	{
		/// <summary>
		/// Return the value or <see cref="Jeebs.Internals.None{T}"/>
		/// </summary>
		/// <typeparam name="TKey">Key type</typeparam>
		/// <typeparam name="TValue">Value type</typeparam>
		/// <param name="dictionary">Dictionary object</param>
		/// <param name="key">Key value</param>
		public static Option<TValue> GetValueOrNone<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) =>
			(dictionary.Count > 0) switch
			{
				true =>
					key switch
					{
						TKey =>
							dictionary.ContainsKey(key) switch
							{
								true when dictionary[key] is TValue value =>
									value,

								true =>
									None<TValue>(new Msg.NullValueMsg<TKey>(key)),

								false =>
									None<TValue>(new Msg.KeyDoesNotExistMsg<TKey>(key))
							},

						_ =>
							None<TValue, Msg.KeyCannotBeNullMsg>()
					},

				false =>
					None<TValue, Msg.DictionaryIsEmptyMsg>()

			};

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>The dictionary is empty</summary>
			public sealed record class DictionaryIsEmptyMsg : IMsg { }

			/// <summary>The dictionary key cannot be null</summary>
			public sealed record class KeyCannotBeNullMsg : IMsg { }

			/// <summary>The specified key does not exist in the dictionary</summary>
			/// <typeparam name="TKey">Key type</typeparam>
			public sealed record class KeyDoesNotExistMsg<TKey>(TKey Key) : IMsg { }

			/// <summary>The dictionary value for the specified key was null</summary>
			/// <typeparam name="TKey">Key type</typeparam>
			public sealed record class NullValueMsg<TKey>(TKey Key) : IMsg { }
		}
	}
}
