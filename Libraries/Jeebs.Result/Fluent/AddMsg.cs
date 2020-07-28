﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Jeebs.Result.Fluent
{
	/// <summary>
	/// Fluently add a message by type to the result chain, returning the original <typeparamref name="TResult"/> object
	/// </summary>
	/// <typeparam name="TResult">Result type</typeparam>
	public class AddMsg<TResult>
		where TResult : IR
	{
		private readonly TResult result;

		internal AddMsg(TResult result)
			=> this.result = result;

		/// <summary>
		/// Add a message of type <typeparamref name="TMessage"/> to the result chain and returns <typeparamref name="TResult"/> object
		/// </summary>
		/// <typeparam name="TMessage">Message type</typeparam>
		public TResult OfType<TMessage>()
			where TMessage : IMsg, new()
		{
			result.Messages.Add<TMessage>();
			return result;
		}
	}
}
