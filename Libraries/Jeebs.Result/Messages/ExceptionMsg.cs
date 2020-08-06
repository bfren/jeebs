using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <inheritdoc/>
	public class ExceptionMsg : IExceptionMsg
	{
		/// <inheritdoc/>
		public string ExceptionType { get; }

		/// <inheritdoc/>
		public string ExceptionText { get; }

		/// <inheritdoc/>
		public string ExceptionTrace { get; }

		/// <summary>
		/// Create object from exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ExceptionMsg(Exception ex)
		{
			ExceptionType = ex.GetType().FullName;
			ExceptionText = ex.Message;
			ExceptionTrace = ex.StackTrace;
		}

		/// <summary>
		/// Output Exception type and message
		/// </summary>
		public override string ToString()
			=> $"{ExceptionType}: {ExceptionText}\nTrace:\n{ExceptionTrace}";
	}
}
