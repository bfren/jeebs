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

			var text = msg.ToString() ?? typeof(T).ToString();

			// Handle exception messages
			if (msg is ExceptionMsg exceptionMsgAbstract)
			{
				if (exceptionMsgAbstract.Level == LogLevel.Fatal)
				{
					Fatal(exceptionMsgAbstract.Exception, text);
				}
				else
				{
					Error(exceptionMsgAbstract.Exception, text);
				}

				return;
			}

			if (msg is IExceptionMsg exceptionMsgInterface)
			{
				Error(exceptionMsgInterface.Exception, text);
				return;
			}

			// Get the log level
			LogLevel level = LogLevel.Information;
			if (msg is ILogMsg loggableMsg)
			{
				level = loggableMsg.Level;
			}

			// Switch different levels
			switch (level)
			{
				case LogLevel.Verbose:
					Verbose(text);
					break;
				case LogLevel.Debug:
					Debug(text);
					break;
				case LogLevel.Information:
					Information(text);
					break;
				case LogLevel.Warning:
					Warning(text);
					break;
				case LogLevel.Error:
					Error(text);
					break;
				case LogLevel.Fatal:
					Fatal(text);
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
