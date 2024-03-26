// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Wrap.Logging;

namespace Jeebs.Logging;

/// <inheritdoc cref="ILog"/>
public abstract class Log : ILog
{
	/// <inheritdoc/>
	public abstract ILog<T> ForContext<T>();

	/// <inheritdoc/>
	public void Failure(FailValue failure) =>
		Failure(failure, failure.Level);

	/// <inheritdoc/>
	public void Failure(FailValue failure, LogLevel level)
	{
		// Get failure message and arguments
		var (text, args) = failure.Context switch
		{
			string context =>
				("{Context} | " + failure.Message, [context, .. failure.Args]),

			_ =>
				(failure.Message, failure.Args ?? [])
		};

		// Switch different levels
		switch (level)
		{
			case LogLevel.Verbose:
				Vrb(text, args);
				break;
			case LogLevel.Debug:
				Dbg(text, args);
				break;
			case LogLevel.Information:
				Inf(text, args);
				break;
			case LogLevel.Warning:
				Wrn(text, args);
				break;
			case LogLevel.Error:
				Err(text, args);
				break;
			case LogLevel.Fatal:
				Ftl(text, args);
				break;
			case LogLevel.Unknown:
			default:
				// Unsupported level
				break;
		}
	}

	/// <inheritdoc/>
	public void Failures(params FailValue[] failures)
	{
		if (failures.Length == 0)
		{
			return;
		}

		foreach (var f in failures)
		{
			Failure(f);
		}
	}

	/// <inheritdoc/>
	public abstract bool IsEnabled(LogLevel level);

	/// <inheritdoc/>
	public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel level) =>
		IsEnabled((LogLevel)level);

	/// <inheritdoc/>
	public abstract void Vrb(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Dbg(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Inf(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Wrn(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Err(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Err(Exception ex, string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Ftl(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Ftl(Exception ex, string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Dispose();
}
