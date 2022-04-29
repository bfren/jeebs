// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages;

/// <summary>
/// Wrapper that contains an inner message
/// </summary>
/// <param name="Value">Inner Message</param>
public abstract record class WithInnerMsg(IMsg Value) : WithValueMsg<IMsg>
{
	/// <inheritdoc/>
	public override string Name { get; init; } = "Inner Message";
}
