// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;

namespace Jeebs.Logging;

/// <inheritdoc cref="ILog"/>
public abstract class Log : ILog
{
	/// <inheritdoc/>
	public abstract ILog<T> ForContext<T>();

	/// <inheritdoc/>
	public void Msg<T>(T? msg)
		where T : IMsg
	{
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
					typeof(T).ToString(),
					Array.Empty<object>()
				)
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
			default:
				// Unsupported level
				break;
		}
	}

	/// <inheritdoc/>
	public void Msgs(params IMsg[] msgs)
	{
		if (msgs.Length == 0)
		{
			return;
		}

		foreach (var msg in msgs)
		{
			Msg(msg);
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
