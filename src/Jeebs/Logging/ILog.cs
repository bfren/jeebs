// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Wrap.Logging;

namespace Jeebs.Logging;

/// <summary>
/// Enables agnostic logging operations.
/// </summary>
/// <typeparam name="TContext">Log context.</typeparam>
public interface ILog<out TContext> : ILog { }

/// <summary>
/// Enables agnostic logging operations.
/// </summary>
public interface ILog
{
	/// <summary>
	/// Return a new log instance for a different context.
	/// </summary>
	/// <typeparam name="T">Log context.</typeparam>
	ILog<T> ForContext<T>();

	/// <summary>
	/// Whether or not the log will write for the specified <paramref name="level"/>.
	/// </summary>
	/// <param name="level">LogLevel.</param>
	/// <returns>Whether or not logging is enabled for the specified <paramref name="level"/>.</returns>
	bool IsEnabled(LogLevel level);

	/// <summary>
	/// Whether or not the log will write for the specified <paramref name="level"/>.
	/// </summary>
	/// <param name="level">LogLevel.</param>
	/// <returns>Whether or not logging is enabled for the specified <paramref name="level"/>.</returns>
	bool IsEnabled(Microsoft.Extensions.Logging.LogLevel level);

	/// <summary>
	/// Log a <see cref="FailureValue"/> using the <see cref="LogLevel"/> it defines.
	/// </summary>
	/// <param name="failure">Failure to log.</param>
	void Failure(FailureValue failure);

	/// <summary>
	/// Log a <see cref="FailureValue"/> using the specified <paramref name="level"/>.
	/// </summary>
	/// <param name="failure">Failure to log.</param>
	/// <param name="level">Log level.</param>
	void Failure(FailureValue failure, LogLevel level);

	/// <summary>
	/// Log an array of <see cref="FailureValue"/> values.
	/// </summary>
	/// <param name="failures">Failures to log.</param>
	void Failures(params FailureValue[] failures);

	/// <inheritdoc cref="LogLevel.Verbose"/>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Vrb(string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Verbose"/>
	/// <param name="ex">Exception.</param>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Vrb(Exception ex, string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Debug"/>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Dbg(string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Debug"/>
	/// <param name="ex">Exception.</param>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Dbg(Exception ex, string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Information"/>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Inf(string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Information"/>
	/// <param name="ex">Exception.</param>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Inf(Exception ex, string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Warning"/>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Wrn(string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Warning"/>
	/// <param name="ex">Exception.</param>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Wrn(Exception ex, string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Error"/>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Err(string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Error"/>
	/// <param name="ex">Exception.</param>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Err(Exception ex, string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Fatal"/>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Ftl(string message, params object?[] args);

	/// <inheritdoc cref="LogLevel.Fatal"/>
	/// <param name="ex">Exception.</param>
	/// <param name="message">Message.</param>
	/// <param name="args">Arguments (if message supports string.Format()).</param>
	void Ftl(Exception ex, string message, params object?[] args);
}
