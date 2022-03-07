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
	bool IsEnabled(LogLevel level);

	/// <summary>
	/// Whether or not the log will write for the specified Level
	/// </summary>
	/// <param name="level">LogLevel</param>
	bool IsEnabled(Microsoft.Extensions.Logging.LogLevel level);

	/// <summary>
	/// Log an <see cref="Jeebs.Msg"/>
	/// </summary>
	/// <typeparam name="T">The message type</typeparam>
	/// <param name="msg">Message to log</param>
	void Msg<T>(T? msg)
		where T : Msg;

	/// <summary>
	/// Log a list of <see cref="Jeebs.Msg"/>
	/// </summary>
	/// <param name="msgs">Messages to log</param>
	void Msg(IEnumerable<Msg> msgs);

	/// <inheritdoc cref="LogLevel.Verbose"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Vrb(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Debug"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Dbg(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Information"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Inf(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Warning"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Wrn(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Error"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Err(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Error"/>
	/// <param name="ex">Exception</param>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Err(Exception ex, string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Fatal"/>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Die(string message, params object[] args);

	/// <inheritdoc cref="LogLevel.Fatal"/>
	/// <param name="ex">Exception</param>
	/// <param name="message">Message</param>
	/// <param name="args">Arguments (if message supports string.Format())</param>
	void Die(Exception ex, string message, params object[] args);
}
