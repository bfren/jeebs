using System;
using Microsoft.Extensions.Logging;

namespace Jeebs
{
	/// <summary>
	/// ILog
	/// </summary>
	public interface ILog
	{
		/// <summary>
		/// Whether or not the log will write for the specified Level
		/// </summary>
		/// <param name="level">LogLevel</param>
		/// <returns>True if the log will write at this level</returns>
		bool IsEnabled(in LogLevel level);

		/// <summary>
		/// Verbose log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Verbose(in string message, params object[] args);

		/// <summary>
		/// Debug log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Debug(in string message, params object[] args);

		/// <summary>
		/// Information log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Information(in string message, params object[] args);

		/// <summary>
		/// Warning log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Warning(in string message, params object[] args);

		/// <summary>
		/// Error log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Error(in string message, params object[] args);

		/// <summary>
		/// Error log message
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Error(in Exception ex, in string message, params object[] args);

		/// <summary>
		/// Fatal log message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Fatal(in string message, params object[] args);

		/// <summary>
		/// Fatal log message
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="message">Message</param>
		/// <param name="args">Arguments (if message supports string.Format())</param>
		void Fatal(in Exception ex, in string message, params object[] args);
	}
}
