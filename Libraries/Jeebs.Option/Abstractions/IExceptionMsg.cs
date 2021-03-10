// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// Represents a framework message for handling exceptions
	/// </summary>
	public interface IExceptionMsg : Logging.ILoggableMsg
	{
		/// <summary>
		/// The exception that occurred
		/// </summary>
		Exception Exception { get; init; }

		/// <summary>
		/// The full name of the Exception type
		/// </summary>
		string ExceptionType { get; }

		/// <summary>
		/// Exception text
		/// </summary>
		string ExceptionText { get; }
	}
}
