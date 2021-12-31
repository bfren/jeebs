// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Logging;

namespace Jeebs;

/// <summary>
/// Enables agnostic logging operations
/// </summary>
/// <typeparam name="TContext">Log context</typeparam>
public interface ILog<TContext> : ILog { }

/// <summary>
/// Enables agnostic logging operations
/// </summary>
public interface ILog
{
	/// <summary>
	/// Return a new log instance for a difference context
	/// </summary>
	/// <typeparam name="T">Log context</typeparam>
	ILog<T> ForContext<T>();

	/// <summary>
	/// Whether or not the log will write for the specified Level
	/// </summary>
	/// <param name="level">LogLevel</param>
	/// <returns>True if the log will write at this level</returns>
	bool IsEnabled(LogLevel level);

	/// <summary>
	/// Whether or not the log will write for the specified Level
	/// </summary>
	/// <param name="level">LogLevel</param>
	/// <returns>True if the log will write at this level</returns>
	bool IsEnabled(Microsoft.Extensions.Logging.LogLevel level);

	/// <summary>
	/// Log an <see cref="Msg"/>
	/// </summary>
	/// <typeparam name="T">The message type</typeparam>
	/// <param name="message">Message to log</param>
	void Message<T>(T? message)
		where T : Msg;

	/// <summary>
	/// Log a list of <see cref="Msg"/>
	/// </summary>
	/// <param name="messages">Messages to log</param>
	void Messages(IEnumerable<Msg> messages);

	/// <inheritdoc cref="LogLevel.Verbose"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Verbose(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Debug"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Debug(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Information"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Information(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Warning"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Warning(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Error"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Error(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Error"/>
	/// <param name="ex">Exception</param>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Error(Exception ex, string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Fatal"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Fatal(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Fatal"/>
	/// <param name="ex">Exception</param>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Fatal(Exception ex, string message, params object[] args);
}
