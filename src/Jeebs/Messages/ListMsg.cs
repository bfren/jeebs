// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;

namespace Jeebs.Messages;

/// <summary>
/// A message containing a list of other <see cref="IMsg"/> objects - useful for building multiple errors
/// </summary>
/// <param name="Messages">Message list</param>
public sealed record class ListMsg(IList<IMsg> Messages) : Msg
{
	/// <summary>
	/// Format messages one per line
	/// </summary>
	public override string Format =>
		"Messages: " + string.Join(System.Environment.NewLine, "{Msg}");

	/// <summary>
	/// Return messages as an array
	/// </summary>
	public override object[]? Args =>
		Messages.ToArray();

	/// <summary>
	/// Create with new messages
	/// </summary>
	/// <param name="msgs">Message list</param>
	public ListMsg(params IMsg[] msgs) : this(msgs.ToList()) { }

	/// <summary>
	/// Add a message to the list
	/// </summary>
	/// <typeparam name="T">Message type</typeparam>
	/// <param name="msg">Message</param>
	public void Add<T>(T msg)
		where T : IMsg =>
		Messages.Add(msg);
}
