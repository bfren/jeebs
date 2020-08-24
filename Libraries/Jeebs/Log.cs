using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Jeebs
{
	/// <inheritdoc cref="ILog"/>
	public abstract class Log : ILog
	{
		/// <inheritdoc/>
		public void Message(IMsg msg)
		{
			// Add message type to the message
			var text = "{MsgType} - " + msg switch
			{
				ILoggableMsg x => x.Format,
				{ } x => x.ToString()
			};

			// Add message type to the argument array
			var args = msg switch
			{
				ILoggableMsg x => x.ParamArray.Prepend(msg.GetType().FullName).ToArray(),
				_ => new object[] { }
			};

			// Handle exception messages
			if (msg is IExceptionMsg exceptionMsg)
			{
				if (exceptionMsg.Level == LogLevel.Critical)
				{
					Critical(exceptionMsg.Exception, text, args);
				}
				else
				{
					Error(exceptionMsg.Exception, text, args);
				}

				return;
			}

			// Get the log level - default is Debug
			LogLevel level = Defaults.Log.Level;
			if (msg is ILoggableMsg loggableMsg)
			{
				level = loggableMsg.Level;
			}

			// Switch different levels
			switch (level)
			{
				case LogLevel.Trace:
					Trace(text, args);
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
				case LogLevel.Critical:
					Critical(text, args);
					break;
				default:
					Debug(text, args);
					break;
			}
		}

		/// <inheritdoc/>
		public abstract bool IsEnabled(LogLevel level);

		/// <inheritdoc/>
		public abstract void Trace(string message, params object[] args);

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
		public abstract void Critical(string message, params object[] args);

		/// <inheritdoc/>
		public abstract void Critical(Exception ex, string message, params object[] args);

		/// <inheritdoc/>
		public abstract void Dispose();
	}
}
