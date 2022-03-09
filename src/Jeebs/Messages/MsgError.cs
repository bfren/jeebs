// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages.Exceptions;
using Maybe;

namespace Jeebs.Messages;

/// <summary>
/// Create <see cref="MsgException{TReason}"/> using a framework message
/// </summary>
public static class MsgError
{
	/// <summary>
	/// Create <see cref="MsgException{TReason}"/>
	/// </summary>
	/// <typeparam name="TReason">Reason type</typeparam>
	/// <param name="msg">Message</param>
	public static MsgException<TReason> CreateException<TReason>(TReason msg)
		where TReason : IReason =>
		new(msg);
}
