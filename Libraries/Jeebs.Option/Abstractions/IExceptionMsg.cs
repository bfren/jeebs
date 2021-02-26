using System;
using System.Collections.Generic;
using System.Text;

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
