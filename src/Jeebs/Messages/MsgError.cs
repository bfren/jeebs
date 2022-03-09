// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages.Exceptions;
using Maybe;

namespace Jeebs.Messages;

/// <summary>
/// Create <see cref="MsgException{T}"/> using an <see cref="IReason"/>
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

	/// <summary>
	/// Create <see cref="MsgException{ReasonMsg}"/>
	/// </summary>
	/// <param name="reason">Reason</param>
	public static MsgException<ReasonMsg> CreateException(IReason reason) =>
		new(new ReasonMsg(reason));
}
