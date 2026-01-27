// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.RegularExpressions;
using Jeebs.Logging.Serilog.MySql.Functions;
using MySqlConnector.Logging;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog.MySql;

/// <summary>
/// Logger implementation for MySqlConnector
/// See https://github.com/mysql-net/MySqlConnector/blob/master/src/MySqlConnector.Logging.Serilog/SerilogLoggerProvider.cs
/// </summary>
public sealed partial class MySqlLogger : IMySqlConnectorLogger
{
	/// <summary>
	/// Logger instance.
	/// </summary>
	internal ILogger Logger { get; private init; }

	/// <summary>
	/// Token replacement regular expression.
	/// </summary>
	internal static Regex TokenReplacer { get; } = LogMessageTokenRegex();

	/// <summary>
	/// Create log instance by name.
	/// </summary>
	/// <param name="name">Log instance name.</param>
	public MySqlLogger(string name) =>
		Logger = global::Serilog.Log.ForContext("SourceContext", "MySqlConnector." + name);

	/// <summary>
	/// Returns true if the log is enabled for <paramref name="level"/>.
	/// </summary>
	/// <param name="level">Requested level.</param>
	public bool IsEnabled(MySqlConnectorLogLevel level) =>
		LevelF.ConvertToSerilogLevel(level).Match(
			some: Logger.IsEnabled,
			none: false
		);

	/// <summary>
	/// Send a message to the log.
	/// </summary>
	/// <param name="level">Event level.</param>
	/// <param name="message">Log message.</param>
	public void Log(MySqlConnectorLogLevel level, string message) =>
		Log(level, message, null, null);

	/// <summary>
	/// Send a message to the log.
	/// </summary>
	/// <param name="level">Event level.</param>
	/// <param name="message">Log message.</param>
	/// <param name="args">[Optional] Message arguments.</param>
	public void Log(MySqlConnectorLogLevel level, string message, object?[]? args) =>
		Log(level, message, args, null);

	/// <summary>
	/// Send a message to the log.
	/// </summary>
	/// <param name="level">Event level.</param>
	/// <param name="message">Log message.</param>
	/// <param name="args">[Optional] Message arguments.</param>
	/// <param name="exception">[Optional] Exception.</param>
	public void Log(MySqlConnectorLogLevel level, string message, object?[]? args, Exception? exception) =>
		LevelF.ConvertToSerilogLevel(level).Match(
			some: x => Log(x, message, args, exception),
			none: () => throw new InvalidOperationException($"Unable to convert {level} to Serilog LogEventLevel.")
		);

	/// <summary>
	/// Send a message to the log.
	/// </summary>
	/// <param name="level">Event level.</param>
	/// <param name="message">Log message.</param>
	/// <param name="args">[Optional] Message arguments.</param>
	/// <param name="exception">[Optional] Exception.</param>
	private void Log(LogEventLevel level, string message, object?[]? args, Exception? exception)
	{
		if (args is null || args.Length == 0)
		{
			if (exception is null)
			{
				Logger.Write(level, message);
			}
			else
			{
				Logger.Write(level, exception, message);
			}
		}
		else
		{
			// rewrite message as template
			var template = TokenReplacer.Replace(message, "$1{MySql$2$3}$4");

			if (exception is null)
			{
				Logger.Write(level, template, args);
			}
			else
			{
				Logger.Write(level, exception, template, args);
			}
		}
	}

	[GeneratedRegex("((\\w+)?\\s?(?:=|:)?\\s?'?)\\{(?:\\d+)(\\:\\w+)?\\}('?)", RegexOptions.Compiled)]
	private static partial Regex LogMessageTokenRegex();
}
