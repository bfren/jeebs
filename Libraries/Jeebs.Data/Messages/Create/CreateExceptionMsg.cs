using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Message about an exception that has occurred during a Create operation
	/// </summary>
	public class CreateExceptionMsg : ExceptionMsg
	{
		private readonly string errorMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		public CreateExceptionMsg(Exception ex, Type type) : base(ex)
			=> errorMessage = new CreateErrorMsg(type).ToString();

		/// <summary>
		/// Output error message plus exception details
		/// </summary>
		public override string ToString()
			=> $"{errorMessage} {base.ToString()} ";
	}
}
