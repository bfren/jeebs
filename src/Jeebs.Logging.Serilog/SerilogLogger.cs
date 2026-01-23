// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using Wrap.Logging;
using S = Serilog;
using SE = Serilog.Events;

namespace Jeebs.Logging.Serilog;

/// <inheritdoc cref="ILog{TContext}"/>
public sealed class SerilogLogger<TContext> : SerilogLogger, ILog<TContext>
{
	/// <summary>
	/// Create logger for <typeparamref name="TContext"/>.
	/// </summary>
	public SerilogLogger() : base(S.Log.ForContext<TContext>()) { }
}

/// <inheritdoc cref="ILog"/>
public class SerilogLogger : Log
{
	/// <summary>
	/// Add this as a prefix to messages logged to the console.
	/// </summary>
	public static string? ConsoleMessagePrefix { get; internal set; }

	private readonly S.ILogger logger;

	/// <summary>
	/// Use global logger.
	/// </summary>
	public SerilogLogger() : this(S.Log.Logger) { }

	internal static string Prefix(string message) =>
		ConsoleMessagePrefix switch
		{
			string app =>
				string.Format(CultureInfo.InvariantCulture, "{0} | {1}", app, message),

			_ =>
				message
		};

	/// <summary>
	/// Use specified logger.
	/// </summary>
	/// <param name="logger">Serilog.ILogger.</param>
	internal SerilogLogger(S.ILogger logger) =>
		this.logger = logger;

	/// <inheritdoc/>
	public override ILog<T> ForContext<T>() =>
		new SerilogLogger<T>();

	/// <inheritdoc/>
	public override bool IsEnabled(LogLevel level) =>
		logger.IsEnabled((SE.LogEventLevel)level);

	/// <inheritdoc/>
	public override void Vrb(string message, object? args = null) =>
		logger.Verbose(Prefix(message), args);

	/// <inheritdoc/>
	public override void Dbg(string message, object? args = null) =>
		logger.Debug(Prefix(message), args);

	/// <inheritdoc/>
	public override void Inf(string message, object? args = null) =>
		logger.Information(Prefix(message), args);

	/// <inheritdoc/>
	public override void Wrn(string message, object? args = null) =>
		logger.Warning(Prefix(message), args);

	/// <inheritdoc/>
	public override void Err(string message, object? args = null) =>
		logger.Error(Prefix(message), args);

	/// <inheritdoc/>
	public override void Err(Exception ex, string message, object? args = null) =>
		logger.Error(ex, Prefix(message), args);

	/// <inheritdoc/>
	public override void Ftl(string message, object? args = null) =>
		logger.Fatal(Prefix(message), args);

	/// <inheritdoc/>
	public override void Ftl(Exception ex, string message, object? args = null) =>
		logger.Fatal(ex, Prefix(message), args);

	/// <inheritdoc/>
	public override void Dispose()
	{
		// Unused
	}
}
