// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Jeebs
{
	/// <summary>
	/// Enables agnostic logging operations
	/// </summary>
	/// <typeparam name="TContext">Log context</typeparam>
	public interface ILog<TContext> : ILog { }

	/// <summary>
	/// Enables agnostic logging operations
	/// </summary>
	public interface ILog : IDisposable
	{
		/// <summary>
		/// Whether or not the log will write for the specified Level
		/// </summary>
		/// <param name="level">LogLevel</param>
		/// <returns>True if the log will write at this level</returns>
		bool IsEnabled(LogLevel level);

		/// <summary>
		/// Log an <see cref="IMsg"/>
		/// </summary>
		/// <param name="message">Message to log</param>
		void Message(IMsg message);

		/// <summary>
		/// Log a list of <see cref="IMsg"/>
		/// </summary>
		/// <param name="messages">Messages to log</param>
		void Messages(IEnumerable<IMsg> messages);

		/// <summary>
		/// Trace log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Trace(string message, params object[] args);

		/// <summary>
		/// Debug log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Debug(string message, params object[] args);

		/// <summary>
		/// Information log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Information(string message, params object[] args);

		/// <summary>
		/// Warning log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Warning(string message, params object[] args);

		/// <summary>
		/// Error log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Error(string message, params object[] args);

		/// <summary>
		/// Error log message
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Error(Exception ex, string message, params object[] args);

		/// <summary>
		/// Critical log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Critical(string message, params object[] args);

		/// <summary>
		/// Critical log message
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Critical(Exception ex, string message, params object[] args);
	}
}
