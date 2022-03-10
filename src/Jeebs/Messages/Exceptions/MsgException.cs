// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Messages.Exceptions;

/// <summary>
/// Throw an exception with an <see cref="IMsg"/>
/// </summary>
/// <typeparam name="TMsg">Reason type</typeparam>
public sealed class MsgException<TMsg> : Exception
	where TMsg : IMsg
{
	/// <summary>
	/// Create exception
	/// </summary>
	public MsgException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="msg"></param>
	public MsgException(TMsg msg) : base(msg switch { ReasonMsg reason => reason.Value.ToString(), _ => msg.ToString() }) { }

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
