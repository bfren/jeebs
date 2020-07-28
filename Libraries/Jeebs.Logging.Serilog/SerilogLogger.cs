using System;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging
{
	/// <inheritdoc cref="ILog"/>
	public sealed class SerilogLogger : ILog
	{
		/// <inheritdoc/>
		public bool IsEnabled(LogLevel level)
			=> Log.Logger.IsEnabled((LogEventLevel)level);

		/// <inheritdoc/>
		public void Verbose(string message, params object[] args)
			=> Log.Logger.Verbose(message, args);

		/// <inheritdoc/>
		public void Debug(string message, params object[] args)
			=> Log.Logger.Debug(message, args);

		/// <inheritdoc/>
		public void Information(string message, params object[] args)
			=> Log.Logger.Information(message, args);

		/// <inheritdoc/>
		public void Warning(string message, params object[] args)
			=> Log.Logger.Warning(message, args);

		/// <inheritdoc/>
		public void Error(string message, params object[] args)
			=> Log.Logger.Error(message, args);

		/// <inheritdoc/>
		public void Error(Exception ex, string message, params object[] args)
			=> Log.Logger.Error(ex, message, args);

		/// <inheritdoc/>
		public void Fatal(string message, params object[] args)
			=> Log.Logger.Fatal(message, args);

		/// <inheritdoc/>
		public void Fatal(Exception ex, string message, params object[] args)
			=> Log.Logger.Fatal(ex, message, args);
	}
}
