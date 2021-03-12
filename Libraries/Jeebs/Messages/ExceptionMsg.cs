// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Logging;

namespace Jm
{
	/// <inheritdoc cref="IExceptionMsg"/>
	public abstract class ExceptionMsg : IExceptionMsg, ILoggableMsg
	{
		/// <inheritdoc/>
		public Exception Exception { get; init; } = new Exception("Unknown.");

		/// <inheritdoc/>
		public string ExceptionType =>
			Exception.GetType().ToString();

		/// <inheritdoc/>
		public string ExceptionText =>
			Exception.Message;

		/// <inheritdoc/>
		public virtual string Format =>
			"{ExceptionType}: {ExceptionText}";

		/// <inheritdoc/>
		public virtual object[] ParamArray =>
			new object[] { ExceptionType, ExceptionText };

		/// <summary>
		/// Log level - default is <see cref="Jeebs.Defaults.Logging.ExceptionLevel"/>
		/// </summary>
		public virtual LogLevel Level =>
			Jeebs.Defaults.Logging.ExceptionLevel;

		/// <summary>
		/// Create blank object
		/// </summary>
		protected ExceptionMsg() { }

		/// <summary>
		/// Create object from exception
		/// </summary>
		/// <param name="ex">Exception</param>
		protected ExceptionMsg(Exception ex) =>
			Exception = ex;

		/// <summary>
		/// Output Exception type and message
		/// </summary>
		public override string ToString() =>
			Format.FormatWith(ParamArray);
	}
}
