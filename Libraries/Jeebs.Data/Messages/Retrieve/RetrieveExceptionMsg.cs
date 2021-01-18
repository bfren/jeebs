using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Message about an exception that has occurred during a Retrieve operation
	/// </summary>
	public class RetrieveExceptionMsg : ExceptionMsg
	{
		private readonly string errorMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public RetrieveExceptionMsg(Exception ex, Type type, long id) : base(ex)
			=> errorMessage = new RetrieveErrorMsg(type, id).ToString();

		/// <summary>
		/// Output error message plus exception details
		/// </summary>
		public override string ToString() =>
			$"{errorMessage} {base.ToString()} ";
	}
}
