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
		public void Message(IMsg? msg)
		{
			if (msg is null)
			{
				return;
			}

			var (text, args) = msg.Prepare();

			// Handle exception messages
			if (msg is IExceptionMsg exceptionMsg)
			{
				if (exceptionMsg.Level == LogLevel.Fatal)
				{
					Fatal(exceptionMsg.Exception, text, args);
				}
				else
				{
					Error(exceptionMsg.Exception, text, args);
				}

				return;
			}

			// Get the log level
			LogLevel level = Defaults.Log.Level;
			if (msg is ILoggableMsg loggableMsg)
			{
				level = loggableMsg.Level;
			}

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
