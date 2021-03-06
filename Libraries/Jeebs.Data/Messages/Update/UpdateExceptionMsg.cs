// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jm.Data
{
	/// <summary>
	/// Message about an exception that has occurred during an update operation
	/// </summary>
	public class UpdateExceptionMsg : ExceptionMsg
	{
		private readonly string errorMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public UpdateExceptionMsg(Exception ex, Type type, long id) : base(ex) =>
			errorMessage = new UpdateErrorMsg(type, id).ToString();

		/// <summary>
		/// Output error message plus exception details
		/// </summary>
		public override string ToString() =>
			$"{errorMessage} {base.ToString()} ";
	}
}
