// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Logging;

namespace Jeebs
{
	/// <inheritdoc cref="IExceptionMsg"/>
	public abstract record ExceptionMsg : LogMsg, IExceptionMsg
	{
		/// <inheritdoc/>
		public Exception Exception { get; init; }

		/// <inheritdoc/>
		public override Func<object[]> Args =>
			() => new object[] { Exception };

		/// <summary>Create blank object</summary>
		protected ExceptionMsg() : this(new Exception("Unknown.")) { }

		/// <summary>Create object from exception</summary>
		/// <param name="ex">Exception</param>
		protected ExceptionMsg(Exception ex) : this(ex, string.Empty) { }

		/// <summary>Create object from exception</summary>
		/// <param name="ex">Exception</param>
		/// <param name="format">Message format (Exception automatically appended)</param>
		protected ExceptionMsg(Exception ex, string format) : this(LogLevel.Error, ex, format) { }

		/// <summary>Create object from exception</summary>
		/// <param name="level">Log Level</param>
		/// <param name="ex">Exception</param>
		/// <param name="format">Message format (Exception automatically appended)</param>
		protected ExceptionMsg(LogLevel level, Exception ex, string format) : base(level, (format + " {Exception}").Trim()) =>
			 Exception = ex;
	}
}
