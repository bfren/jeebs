// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using MS = Microsoft.Extensions.Logging;

namespace Jeebs
{
	/// <inheritdoc/>
	public sealed class Logger : ILogger
	{
		internal ILog? log;

		/// <inheritdoc/>
		public bool IsEnabled(MS.LogLevel level) =>
			log?.IsEnabled(level) ?? false;

		/// <inheritdoc/>
		public void Message(IMsg? message) =>
			log?.Message(message);

		/// <inheritdoc/>
		public void Messages(IEnumerable<IMsg> messages) =>
			log?.Messages(messages);

		/// <inheritdoc/>
		public void Trace(string message, params object[] args) =>
			log?.Trace(message, args);

		/// <inheritdoc/>
		public void Debug(string message, params object[] args) =>
			log?.Debug(message, args);

		/// <inheritdoc/>
		public void Information(string message, params object[] args) =>
			log?.Information(message, args);

		/// <inheritdoc/>
		public void Warning(string message, params object[] args) =>
			log?.Warning(message, args);

		/// <inheritdoc/>
		public void Error(string message, params object[] args) =>
			log?.Error(message, args);

		/// <inheritdoc/>
		public void Error(Exception ex, string message, params object[] args) =>
			log?.Error(ex, message, args);

		/// <inheritdoc/>
		public void Critical(string message, params object[] args) =>
			log?.Critical(message, args);

		/// <inheritdoc/>
		public void Critical(Exception ex, string message, params object[] args) =>
			log?.Critical(ex, message, args);

		/// <inheritdoc/>
		public void Dispose() =>
			log?.Dispose();
	}
}
