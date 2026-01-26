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
	public void Failure(FailureValue failure) =>
		Failure(failure, failure.Level);

	/// <inheritdoc/>
	public void Failure(FailureValue failure, LogLevel level)
	{
		// Add context to message
		var text = failure.Context switch
		{
			string context =>
				$"{context} | " + failure.Message,

			_ =>
				failure.Message
		};

		// Switch different levels
		switch (level)
		{
			case LogLevel.Verbose when failure.Exception is not null:
				Vrb(failure.Exception, text, failure.Args);
				break;
			case LogLevel.Verbose:
				Vrb(text, failure.Args);
				break;
			case LogLevel.Debug when failure.Exception is not null:
				Dbg(failure.Exception, text, failure.Args);
				break;
			case LogLevel.Debug:
				Dbg(text, failure.Args);
				break;
			case LogLevel.Information when failure.Exception is not null:
				Inf(failure.Exception, text, failure.Args);
				break;
			case LogLevel.Information:
				Inf(text, failure.Args);
				break;
			case LogLevel.Warning when failure.Exception is not null:
				Wrn(failure.Exception, text, failure.Args);
				break;
			case LogLevel.Warning:
				Wrn(text, failure.Args);
				break;
			case LogLevel.Error when failure.Exception is not null:
				Err(failure.Exception, text, failure.Args);
				break;
			case LogLevel.Error:
				Err(text, failure.Args);
				break;
			case LogLevel.Fatal when failure.Exception is not null:
				Ftl(failure.Exception, text, failure.Args);
				break;
			case LogLevel.Fatal:
				Ftl(text, failure.Args);
				break;
			case LogLevel.Unknown:
			default:
				// Unsupported level
				break;
		}
	}

	/// <inheritdoc/>
	public void Failures(params FailureValue[] failures)
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
	public abstract void Vrb(string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Vrb(Exception ex, string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Dbg(string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Dbg(Exception ex, string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Inf(string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Inf(Exception ex, string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Wrn(string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Wrn(Exception ex, string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Err(string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Err(Exception ex, string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Ftl(string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Ftl(Exception ex, string message, params object?[] args);

	/// <inheritdoc/>
	public abstract void Dispose();
}
