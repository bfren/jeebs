// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Logging;

namespace Jeebs
{
	/// <inheritdoc cref="ILog"/>
	public abstract class Log : ILog
	{
		/// <inheritdoc/>
		public void Message<T>(T? msg)
			where T : IMsg
		{
			if (msg is null)
			{
				return;
			}

			// Get log info
			var (level, text, args) = msg switch
			{
				ILogMsg loggable =>
					(
						loggable.Level,
						loggable.Format,
						getArgs(loggable)
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
			}

			// Get arguments from a loggable message
			static object[] getArgs(ILogMsg msg)
			{
				// Get args and add message to the start of the array
				var list = msg.Args().ToList();
				list.Insert(0, typeof(T));

				// Add Exception to the end of the array
				if (msg is IExceptionMsg exceptionMsg)
				{
					list.Add(exceptionMsg.Exception);
				}

				// Convert back to an array
				return list.ToArray();
			}
		}

		/// <inheritdoc/>
		public void Messages(IEnumerable<IMsg> messages)
		{
			if (!messages.Any())
			{
				return;
			}

			foreach (var item in messages)
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
}
