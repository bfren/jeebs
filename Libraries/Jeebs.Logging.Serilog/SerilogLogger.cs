using System;
using MS = Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Jeebs.Logging
{
	/// <inheritdoc cref="ILog{TContext}"/>
	public class SerilogLogger<TContext> : SerilogLogger, ILog<TContext>
	{
		/// <summary>
		/// Create logger for <typeparamref name="TContext"/>
		/// </summary>
		public SerilogLogger() : base(Log.ForContext<TContext>()) { }
	}

	/// <inheritdoc cref="ILog"/>
	public class SerilogLogger : ILog
	{
		private readonly ILogger logger;

		/// <summary>
		/// Use global logger
		/// </summary>
		public SerilogLogger()
			=> logger = Log.Logger;

		/// <summary>
		/// Use specified logger
		/// </summary>
		/// <param name="logger">Serilog.ILogger</param>
		internal SerilogLogger(ILogger logger)
			=> this.logger = logger;

		/// <inheritdoc/>
		public bool IsEnabled(MS.LogLevel level)
			=> logger.IsEnabled((LogEventLevel)level);

		/// <inheritdoc/>
		public void Message(IMsg msg)
		{
			if (msg is IExceptionMsg exceptionMsg)
			{
				if (exceptionMsg.Level == MS.LogLevel.Critical)
				{
					Critical(exceptionMsg.Exception, exceptionMsg.Format, exceptionMsg.ParamArray);
				}
				else
				{
					Error(exceptionMsg.Exception, exceptionMsg.Format, exceptionMsg.ParamArray);
				}
			}
			else if (msg is ILoggableMsg loggableMsg)
			{
				send(loggableMsg.Level, loggableMsg.Format, loggableMsg.ParamArray);
			}
			else
			{
				send(MS.LogLevel.Trace, msg.ToString());
			}

			void send(MS.LogLevel level, string message, params object[] args)
			{
				switch (level)
				{
					case MS.LogLevel.Debug:
						Debug(message, args);
						break;
					case MS.LogLevel.Information:
						Information(message, args);
						break;
					case MS.LogLevel.Warning:
						Warning(message, args);
						break;
					case MS.LogLevel.Error:
						Error(message, args);
						break;
					case MS.LogLevel.Critical:
						Critical(new Exception("Unknown caller."), message, args);
						break;
					default:
						Trace(message, args);
						break;
				}
			}
		}

		/// <inheritdoc/>
		public void Trace(string message, params object[] args)
			=> logger.Verbose(message, args);

		/// <inheritdoc/>
		public void Debug(string message, params object[] args)
			=> logger.Debug(message, args);

		/// <inheritdoc/>
		public void Information(string message, params object[] args)
			=> logger.Information(message, args);

		/// <inheritdoc/>
		public void Warning(string message, params object[] args)
			=> logger.Warning(message, args);

		/// <inheritdoc/>
		public void Error(string message, params object[] args)
			=> logger.Error(message, args);

		/// <inheritdoc/>
		public void Error(Exception ex, string message, params object[] args)
			=> logger.Error(ex, message, args);

		/// <inheritdoc/>
		public void Fatal(string message, params object[] args)
			=> logger.Fatal(message, args);

		/// <inheritdoc/>
		public void Critical(Exception ex, string message, params object[] args)
			=> logger.Fatal(ex, message, args);

		/// <inheritdoc/>
		public void Dispose()
			=> Log.CloseAndFlush();
	}
}
