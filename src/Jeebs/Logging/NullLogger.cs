// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Logging;

/// <inheritdoc cref="NullLogger"/>
public sealed class NullLogger<T> : NullLogger, ILog<T> { }

/// <summary>
/// A logger that does nothing - except stop null reference exceptions
/// </summary>
public class NullLogger : Log
{
	/// <inheritdoc/>
	public override ILog<T> ForContext<T>() =>
		new NullLogger<T>();

	/// <inheritdoc/>
	public override bool IsEnabled(LogLevel level) =>
		false;

	/// <inheritdoc/>
	public override void Vrb(string message, params object[] args) { }

	/// <inheritdoc/>
	public override void Dbg(string message, params object[] args) { }

	/// <inheritdoc/>
	public override void Inf(string message, params object[] args) { }

	/// <inheritdoc/>
	public override void Wrn(string message, params object[] args) { }

	/// <inheritdoc/>
	public override void Err(string message, params object[] args) { }

	/// <inheritdoc/>
	public override void Err(Exception ex, string message, params object[] args) { }

	/// <inheritdoc/>
	public override void Ftl(string message, params object[] args) { }

	/// <inheritdoc/>
	public override void Ftl(Exception ex, string message, params object[] args) { }

	/// <inheritdoc/>
	public override void Dispose() { }
}
