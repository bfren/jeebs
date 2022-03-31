// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages.Exceptions;

namespace Jeebs.Messages;

/// <summary>
/// Create <see cref="MsgException{T}"/> using an <see cref="IMsg"/>
/// </summary>
public static class MsgError
{
	/// <summary>
	/// Create <see cref="MsgException{TMsg}"/>
	/// </summary>
	/// <typeparam name="TMsg">Message type</typeparam>
	/// <param name="msg">Message</param>
	public static MsgException<TMsg> CreateException<TMsg>(TMsg msg)
		where TMsg : IMsg =>
		new(msg);
}
