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
	public void Failure(FailureValue value) =>
		Failure(value, value.Level);

	/// <inheritdoc/>
	public void Failure(FailureValue value, LogLevel level)
	{
		// Add context to message
		var text = value.Context switch
		{
			string context =>
				$"{context} | " + value.Message,

			_ =>
				value.Message
		};

		// Switch different levels
		switch (level)
		{
			case LogLevel.Verbose when value.Exception is not null:
				Vrb(value.Exception, text, value.Args);
				break;
			case LogLevel.Verbose:
				Vrb(text, value.Args);
				break;
			case LogLevel.Debug when value.Exception is not null:
				Dbg(value.Exception, text, value.Args);
				break;
			case LogLevel.Debug:
				Dbg(text, value.Args);
				break;
			case LogLevel.Information when value.Exception is not null:
				Inf(value.Exception, text, value.Args);
				break;
			case LogLevel.Information:
				Inf(text, value.Args);
				break;
			case LogLevel.Warning when value.Exception is not null:
				Wrn(value.Exception, text, value.Args);
				break;
			case LogLevel.Warning:
				Wrn(text, value.Args);
				break;
			case LogLevel.Error when value.Exception is not null:
				Err(value.Exception, text, value.Args);
				break;
			case LogLevel.Error:
				Err(text, value.Args);
				break;
			case LogLevel.Fatal when value.Exception is not null:
				Ftl(value.Exception, text, value.Args);
				break;
			case LogLevel.Fatal:
				Ftl(text, value.Args);
				break;
			case LogLevel.Unknown:
			default:
				// Unsupported level
				break;
		}
	}

	/// <inheritdoc/>
	public void Failures(params FailureValue[] values)
	{
		if (values.Length == 0)
		{
			return;
		}

		foreach (var f in values)
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
