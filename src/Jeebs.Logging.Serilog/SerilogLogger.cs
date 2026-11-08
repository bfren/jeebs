// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Logging.Serilog.Functions;
using Wrap.Logging;
using S = Serilog;

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
	private readonly S.ILogger logger;

	/// <summary>
	/// Use global logger.
	/// </summary>
	public SerilogLogger() : this(S.Log.Logger) { }

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
		LevelF.ConvertToSerilogLevel(level).Match(
			fFail: _ => false,
			fOk: logger.IsEnabled
		);

	/// <inheritdoc/>
	public override void Vrb(string message, params object?[] args) =>
		logger.Verbose(message, args);

	/// <inheritdoc/>
	public override void Vrb(Exception ex, string message, params object?[] args) =>
		logger.Verbose(ex, message, args);

	/// <inheritdoc/>
	public override void Dbg(string message, params object?[] args) =>
		logger.Debug(message, args);

	/// <inheritdoc/>
	public override void Dbg(Exception ex, string message, params object?[] args) =>
		logger.Debug(ex, message, args);

	/// <inheritdoc/>
	public override void Inf(string message, params object?[] args) =>
		logger.Information(message, args);

	/// <inheritdoc/>
	public override void Inf(Exception ex, string message, params object?[] args) =>
		logger.Information(ex, message, args);

	/// <inheritdoc/>
	public override void Wrn(string message, params object?[] args) =>
		logger.Warning(message, args);

	/// <inheritdoc/>
	public override void Wrn(Exception ex, string message, params object?[] args) =>
		logger.Warning(ex, message, args);

	/// <inheritdoc/>
	public override void Err(string message, params object?[] args) =>
		logger.Error(message, args);

	/// <inheritdoc/>
	public override void Err(Exception ex, string message, params object?[] args) =>
		logger.Error(ex, message, args);

	/// <inheritdoc/>
	public override void Ftl(string message, params object?[] args) =>
		logger.Fatal(message, args);

	/// <inheritdoc/>
	public override void Ftl(Exception ex, string message, params object?[] args) =>
		logger.Fatal(ex, message, args);

	/// <inheritdoc/>
	public override void Dispose()
	{
		// Unused
	}
}
