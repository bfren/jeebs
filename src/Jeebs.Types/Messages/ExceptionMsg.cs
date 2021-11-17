// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

/// <summary>
/// Framework message for handling exceptions
/// </summary>
public abstract record class ExceptionMsg : WithValueMsg<Exception>
{
	/// <inheritdoc/>
	public override string Name =>
		"Exception";
}
