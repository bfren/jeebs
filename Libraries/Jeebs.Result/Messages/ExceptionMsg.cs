using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Exception handling message
	/// </summary>
	public class ExceptionMsg : IMsg
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
		/// Create object from exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ExceptionMsg(Exception ex)
		{
			ExceptionType = ex.GetType().FullName;
			ExceptionText = ex.Message;
		}

		/// <summary>
		/// Output Exception type and message
		/// </summary>
		public override string ToString()
			=> $"{ExceptionType}: {ExceptionText}";
	}
}
