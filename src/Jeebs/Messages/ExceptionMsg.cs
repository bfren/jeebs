// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Logging;

namespace Jeebs.Messages;

/// <inheritdoc cref="IExceptionMsg"/>
public abstract record class ExceptionMsg : WithValueMsg<Exception>, IExceptionMsg
{
	/// <summary>
	/// Override Level with <see cref="LogLevel.Error"/>
	/// </summary>
	public override LogLevel Level { get; protected init; } = LogLevel.Error;

	/// <summary>
	/// Override Name with 'Exception'
	/// </summary>
	public override string Name { get; init; } = "Exception";
}
