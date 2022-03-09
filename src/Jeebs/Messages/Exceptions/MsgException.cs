// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Maybe;

namespace Jeebs.Messages.Exceptions;

/// <summary>
/// Throw an exception with an <see cref="IReason"/>
/// </summary>
/// <typeparam name="TReason">Reason type</typeparam>
public sealed class MsgException<TReason> : Exception
	where TReason : IReason
{
	/// <summary>
	/// Create exception
	/// </summary>
	public MsgException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="reason"></param>
	public MsgException(TReason reason) : base(reason.ToString()) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message">Message</param>
	public MsgException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message">Message</param>
	/// <param name="innerException">Inner Exception</param>
	public MsgException(string message, Exception innerException) : base(message, innerException) { }
}
