using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging
{
	/// <inheritdoc cref="ILog{TContext}"/>
	public class SerilogLogger<TContext> : SerilogLogger, ILog<TContext>
	{
		/// <summary>
		/// Create logger for <typeparamref name="TContext"/>
		/// </summary>
		public SerilogLogger() : base(Serilog.Log.ForContext<TContext>()) { }
	}

	/// <inheritdoc cref="ILog"/>
	public class SerilogLogger : Log
	{
		private readonly ILogger logger;

		/// <summary>
		/// Use global logger
		/// </summary>
		public SerilogLogger() : this(Serilog.Log.Logger) { }

		/// <summary>
		/// Use specified logger
		/// </summary>
		/// <param name="logger">Serilog.ILogger</param>
		internal SerilogLogger(ILogger logger) =>
			this.logger = logger;

		/// <inheritdoc/>
		public override bool IsEnabled(Microsoft.Extensions.Logging.LogLevel level) =>
			logger.IsEnabled((LogEventLevel)level);

		/// <inheritdoc/>
		public override void Trace(string message, params object[] args) =>
			logger.Verbose(message, args);

		/// <inheritdoc/>
		public override void Debug(string message, params object[] args) =>
			logger.Debug(message, args);

		/// <inheritdoc/>
		public override void Information(string message, params object[] args) =>
			logger.Information(message, args);

		/// <inheritdoc/>
		public override void Warning(string message, params object[] args) =>
			logger.Warning(message, args);

		/// <inheritdoc/>
		public override void Error(string message, params object[] args) =>
			logger.Error(message, args);

		/// <inheritdoc/>
		public override void Error(Exception ex, string message, params object[] args) =>
			logger.Error(ex, message, args);

		/// <inheritdoc/>
		public override void Critical(string message, params object[] args) =>
			logger.Fatal(message, args);

		/// <inheritdoc/>
		public override void Critical(Exception ex, string message, params object[] args) =>
			logger.Fatal(ex, message, args);

		/// <inheritdoc/>
		public override void Dispose() { }
	}
}
