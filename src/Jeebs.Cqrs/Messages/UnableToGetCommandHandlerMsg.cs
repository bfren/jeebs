// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;

namespace Jeebs.Cqrs.Messages;

/// <summary>Unable to get command handler</summary>
/// <param name="Value">Command Type</param>
public sealed record class UnableToGetCommandHandlerMsg(Type Value) : WithValueMsg<Type>
{
	/// <summary>Change value name to 'Command Type'</summary>
	public override string Name =>
		"Command Type";
}
