// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.RegularExpressions;
using MySqlConnector.Logging;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog.MySql;

/// <summary>
/// Logger implementation for MySqlConnector
/// See https://github.com/mysql-net/MySqlConnector/blob/master/src/MySqlConnector.Logging.Serilog/SerilogLoggerProvider.cs
/// </summary>
public sealed class MySqlLogger : IMySqlConnectorLogger
{
	/// <summary>
	/// Logger instance
	/// </summary>
	internal ILogger Logger { get; private init; }

	/// <summary>
	/// Minimum log level
	/// </summary>
	internal LogEventLevel Minimum { get; private init; }

	/// <summary>
	/// Token replacement regular expression
	/// </summary>
	internal static Regex TokenReplacer { get; } = new(@"((\w+)?\s?(?:=|:)?\s?'?)\{(?:\d+)(\:\w+)?\}('?)", RegexOptions.Compiled);

	/// <summary>
	/// Create log instance by name
	/// </summary>
	/// <param name="name">Log instance name</param>
	/// <param name="minimum">Minimum log level</param>
	public MySqlLogger(string name, LogEventLevel minimum) =>
		(Logger, Minimum) = (global::Serilog.Log.ForContext("SourceContext", "MySqlConnector." + name), minimum);

	/// <summary>
	/// Returns true if the log is enable for <paramref name="level"/>
	/// </summary>
	/// <param name="level">Requested level</param>
	public bool IsEnabled(MySqlConnectorLogLevel level)
	{
		var requestedLevel = GetLevel(level);
		return requestedLevel >= Minimum && Logger.IsEnabled(requestedLevel);
	}

	/// <summary>
	/// Send a message to the log
	/// </summary>
	/// <param name="level">Event level</param>
	/// <param name="message">Log message</param>
	/// <param name="args">[Optional] Message arguments</param>
	/// <param name="exception">[Optional] Exception</param>
	public void Log(MySqlConnectorLogLevel level, string message, object?[]? args = null, Exception? exception = null)
	{
		var requestedLevel = GetLevel(level);
		if (requestedLevel < Minimum)
		{
			return;
		}

		if (args is null || args.Length == 0)
		{
			if (exception is null)
			{
				Logger.Write(requestedLevel, message);
			}
			else
			{
				Logger.Write(requestedLevel, exception, message);
			}
		}
		else
		{
			// rewrite message as template
			var template = TokenReplacer.Replace(message, "$1{MySql$2$3}$4");

			if (exception is null)
			{
				Logger.Write(requestedLevel, template, args);
			}
			else
			{
				Logger.Write(requestedLevel, exception, template, args);
			}
		}
	}

	/// <summary>
	/// Convert <see cref="MySqlConnectorLogLevel"/> to <see cref="LogEventLevel"/>
	/// </summary>
	/// <param name="level">Level to be converted</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	internal static LogEventLevel GetLevel(MySqlConnectorLogLevel level) => level switch
	{
		MySqlConnectorLogLevel.Trace =>
			LogEventLevel.Verbose,

		MySqlConnectorLogLevel.Debug =>
			LogEventLevel.Debug,

		MySqlConnectorLogLevel.Info =>
			LogEventLevel.Information,

		MySqlConnectorLogLevel.Warn =>
			LogEventLevel.Warning,

		MySqlConnectorLogLevel.Error =>
			LogEventLevel.Error,

		MySqlConnectorLogLevel.Fatal =>
			LogEventLevel.Fatal,

		_ =>
			throw new ArgumentOutOfRangeException(nameof(level), level, "Invalid value for 'level'."),
	};
}
