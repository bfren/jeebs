// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Logging;

namespace Jeebs
{
	/// <inheritdoc cref="IExceptionMsg"/>
	public abstract record ExceptionMsg : IExceptionMsg, ILogMsg
	{
		/// <inheritdoc/>
		public Exception Exception { get; init; }

		/// <inheritdoc/>
		public virtual LogLevel Level =>
			LogLevel.Error;

		/// <summary>Create blank object</summary>
		protected ExceptionMsg() : this(new Exception("Unknown.")) { }

		/// <summary>Create object from exception</summary>
		/// <param name="ex">Exception</param>
		protected ExceptionMsg(Exception ex) =>
			Exception = ex;
	}
}
