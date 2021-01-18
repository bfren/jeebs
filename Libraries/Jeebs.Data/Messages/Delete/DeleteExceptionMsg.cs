﻿using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Message about an exception that has occurred during a Delete operation
	/// </summary>
	public class DeleteExceptionMsg : ExceptionMsg
	{
		private readonly string errorMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteExceptionMsg(Exception ex, Type type, long id) : base(ex) =>
			errorMessage = new DeleteErrorMsg(type, id).ToString();

		/// <summary>
		/// Output error message plus exception details
		/// </summary>
		public override string ToString() =>
			$"{errorMessage} {base.ToString()} ";
	}
}
