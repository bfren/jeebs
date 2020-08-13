using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Exception handling message
	/// </summary>
	public interface IExceptionMsg : IMsg
	{
		/// <summary>
		/// The full name of the Exception type
		/// </summary>
		string ExceptionType { get; }

		/// <summary>
		/// Exception text
		/// </summary>
		string ExceptionText { get; }

		/// <summary>
		/// Exception trace
		/// </summary>
		string ExceptionTrace { get; }

		/// <summary>
		/// Set properties from exception
		/// </summary>
		/// <param name="ex">Exception</param>
		void Set(Exception ex);
	}
}
