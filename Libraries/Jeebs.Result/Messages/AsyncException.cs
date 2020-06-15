using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Exception handling message
	/// </summary>
	public class AsyncException : IMessage
	{
		/// <summary>
		/// The full name of the Exception type
		/// </summary>
		private readonly string exceptionType;

		/// <summary>
		/// Exception text
		/// </summary>
		private readonly string exceptionText;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		public AsyncException(System.Exception ex)
		{
			exceptionType = ex.GetType().FullName;
			exceptionText = ex.Message;
		}

		/// <summary>
		/// Output Exception type and message
		/// </summary>
		public override string ToString() => $"{exceptionType}: {exceptionText}";
	}
}
