// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages;

/// <summary>
/// Wraps an <see cref="IReason"/> in an <see cref="IMsg"/>
/// </summary>
/// <param name="Value">Reason</param>
public sealed record class ReasonMsg(IReason Value) : WithValueMsg<IReason>;
