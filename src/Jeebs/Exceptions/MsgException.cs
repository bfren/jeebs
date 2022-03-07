// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

/// <inheritdoc cref="MsgException{TMsg}"/>
public static class MsgError
{
	/// <summary>
	/// Create <see cref="MsgException{TMsg}"/>
	/// </summary>
	/// <typeparam name="TMsg">Message type</typeparam>
	/// <param name="msg">Message</param>
	public static MsgException<TMsg> CreateException<TMsg>(TMsg msg)
		where TMsg : Msg =>
		new(msg);
}

/// <summary>
/// Throw an exception with an <see cref="Msg"/>
/// </summary>
/// <typeparam name="TMsg">Message type</typeparam>
public sealed class MsgException<TMsg> : Exception
	where TMsg : Msg
{
	/// <summary>
	/// Create exception
	/// </summary>
	public MsgException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="msg">Message</param>
	public MsgException(TMsg msg) : base(msg.ToString()) { }

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
