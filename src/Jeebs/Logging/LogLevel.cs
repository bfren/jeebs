// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Logging;

/// <summary>
/// Log Levels - descriptions taken from https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.loglevel?view=dotnet-plat-ext-5.0
/// </summary>
public enum LogLevel
{
	/// <summary>
	/// Logs that contain the most detailed messages.
	/// These messages may contain sensitive application data.
	/// These messages are disabled by default and should never be enabled in a production environment.
	/// </summary>
	Verbose = 0, // value matches Serilog.Events.LogEventLevel.Verbose

	/// <summary>
	/// Logs that are used for interactive investigation during development.
	/// These logs should primarily contain information useful for debugging and have no long-term value.
	/// </summary>
	Debug = 1, // value matches Serilog.Events.LogEventLevel.Debug

	/// <summary>
	/// Logs that track the general flow of the application. These logs should have long-term value.
	/// </summary>
	Information = 2, // value matches Serilog.Events.LogEventLevel.Information

	/// <summary>
	/// Logs that highlight an abnormal or unexpected event in the application flow,
	/// but do not otherwise cause the application execution to stop.
	/// </summary>
	Warning = 3, // value matches Serilog.Events.LogEventLevel.Warning

	/// <summary>
	/// Logs that highlight when the current flow of execution is stopped due to a failure.
	/// These should indicate a failure in the current activity, not an application-wide failure.
	/// </summary>
	Error = 4, // value matches Serilog.Events.LogEventLevel.Error

	/// <summary>
	/// Logs that describe an unrecoverable application or system crash,
	/// or a catastrophic failure that requires immediate attention.
	/// </summary>
	Fatal = 5 // value matches Serilog.Events.LogEventLevel.Fatal
}
