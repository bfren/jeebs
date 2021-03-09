// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
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
		/// <summary>
		/// Add this as a prefix to messages logged to the console
		/// </summary>
		public static string? ConsoleMessagePrefix { get; internal set; }

		private readonly ILogger logger;

		/// <summary>
		/// Use global logger
		/// </summary>
		public SerilogLogger() : this(Serilog.Log.Logger) { }

		internal static string Prefix(string message) =>
			ConsoleMessagePrefix switch
			{
				string app =>
					string.Format("{0} | {1}", app, message),

				_ =>
					message
			};

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
			logger.Verbose(Prefix(message), args);

		/// <inheritdoc/>
		public override void Debug(string message, params object[] args) =>
			logger.Debug(Prefix(message), args);

		/// <inheritdoc/>
		public override void Information(string message, params object[] args) =>
			logger.Information(Prefix(message), args);

		/// <inheritdoc/>
		public override void Warning(string message, params object[] args) =>
			logger.Warning(Prefix(message), args);

		/// <inheritdoc/>
		public override void Error(string message, params object[] args) =>
			logger.Error(Prefix(message), args);

		/// <inheritdoc/>
		public override void Error(Exception ex, string message, params object[] args) =>
			logger.Error(ex, Prefix(message), args);

		/// <inheritdoc/>
		public override void Critical(string message, params object[] args) =>
			logger.Fatal(Prefix(message), args);

		/// <inheritdoc/>
		public override void Critical(Exception ex, string message, params object[] args) =>
			logger.Fatal(ex, Prefix(message), args);

		/// <inheritdoc/>
		public override void Dispose() { }
	}
}
