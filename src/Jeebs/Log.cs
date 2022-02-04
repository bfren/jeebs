// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Logging;

namespace Jeebs;

/// <inheritdoc cref="ILog"/>
public abstract class Log : ILog
{
	/// <inheritdoc/>
	public abstract ILog<T> ForContext<T>();

	/// <inheritdoc/>
	public void Message<T>(T? msg)
		where T : Msg
	{
		if (msg is null)
		{
			return;
		}

		// Get log info
		var (level, text, args) = msg switch
		{
			Msg m =>
				(
					m.Level,
					m.FormatWithType,
					m.ArgsWithType
				),

			_ =>
				(
					LogLevel.Information,
					msg.ToString() ?? typeof(T).ToString(),
					Array.Empty<object>()
				)
		};

		// Switch different levels
		switch (level)
		{
			case LogLevel.Verbose:
				Verbose(text, args);
				break;
			case LogLevel.Debug:
				Debug(text, args);
				break;
			case LogLevel.Information:
				Information(text, args);
				break;
			case LogLevel.Warning:
				Warning(text, args);
				break;
			case LogLevel.Error:
				Error(text, args);
				break;
			case LogLevel.Fatal:
				Fatal(text, args);
				break;
			default:
				// Unsupported level
				break;
		}
	}

	/// <inheritdoc/>
	public void Messages(IEnumerable<Msg> msgs)
	{
		if (!msgs.Any())
		{
			return;
		}

		foreach (var item in msgs)
		{
			Message(item);
		}
	}

	/// <inheritdoc/>
	public abstract bool IsEnabled(LogLevel level);

	/// <inheritdoc/>
	public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel level) =>
		IsEnabled((LogLevel)level);

	/// <inheritdoc/>
	public abstract void Verbose(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Debug(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Information(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Warning(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Error(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Error(Exception ex, string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Fatal(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Fatal(Exception ex, string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Dispose();
}
