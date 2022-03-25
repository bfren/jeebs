// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;

namespace Jeebs.Cqrs.Messages;

/// <summary>Unable to get query handler</summary>
/// <param name="Value">Query Type</param>
public sealed record class UnableToGetQueryHandlerMsg(Type Value) : WithValueMsg<Type>
{
	/// <summary>Change value name to 'Query Type'</summary>
	public override string Name { get; init; } = "Query Type";
}
