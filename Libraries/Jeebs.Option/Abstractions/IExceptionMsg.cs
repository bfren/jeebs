// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// Represents a framework message for handling exceptions
	/// </summary>
	public interface IExceptionMsg : ILoggableMsg
	{
		/// <summary>
		/// The exception that occurred
		/// </summary>
		public Exception Exception { get; }

		/// <summary>
		/// The full name of the Exception type
		/// </summary>
		string ExceptionType { get; }

		/// <summary>
		/// Exception text
		/// </summary>
		string ExceptionText { get; }

		/// <summary>
		/// Set properties from exception
		/// </summary>
		/// <param name="ex">Exception</param>
		void Set(Exception ex);
	}
}
