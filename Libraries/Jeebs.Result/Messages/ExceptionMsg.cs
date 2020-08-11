using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <inheritdoc cref="IExceptionMsg"/>
	public class ExceptionMsg : IExceptionMsg
	{
		/// <inheritdoc/>
		public string ExceptionType { get; private set; } = string.Empty;

		/// <inheritdoc/>
		public string ExceptionText { get; private set; } = string.Empty;

		/// <inheritdoc/>
		public string ExceptionTrace { get; private set; } = string.Empty;

		/// <summary>
		/// Properties must then be set using <see cref="Set(Exception)"/>
		/// </summary>
		public ExceptionMsg() { }

		/// <summary>
		/// Create object from exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ExceptionMsg(Exception ex)
			=> Set(ex);

		/// <inheritdoc/>
		public void Set(Exception ex)
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
