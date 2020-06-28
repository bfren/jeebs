using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Exception handling message
	/// </summary>
	public class Exception : IMessage
	{
		/// <summary>
		/// The full name of the Exception type
		/// </summary>
		protected string ExceptionType { get; }

		/// <summary>
		/// Exception text
		/// </summary>
		protected string ExceptionText { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		public Exception(System.Exception ex)
		{
			ExceptionType = ex.GetType().FullName;
			ExceptionText = ex.Message;
		}

		/// <summary>
		/// Output Exception type and message
		/// </summary>
		public override string ToString() => $"{ExceptionType}: {ExceptionText}";
	}
}
