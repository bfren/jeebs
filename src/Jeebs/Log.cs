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
	public void Msg<T>(T? msg)
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
				Die(text, args);
				break;
			default:
				// Unsupported level
				break;
		}
	}

	/// <inheritdoc/>
	public void Msg(IEnumerable<Msg> msgs)
	{
		if (!msgs.Any())
		{
			return;
		}

		foreach (var item in msgs)
		{
			Msg(item);
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
	public abstract void Die(string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Die(Exception ex, string message, params object[] args);

	/// <inheritdoc/>
	public abstract void Dispose();
}
