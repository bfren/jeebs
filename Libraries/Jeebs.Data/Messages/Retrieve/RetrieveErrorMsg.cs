// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jm.Data
{
	/// <summary>
	/// Message about an error that has occurred during a Retrieve operation
	/// </summary>
	public class RetrieveErrorMsg : RetrieveMsg
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public RetrieveErrorMsg(Type type, long id) : base(type, id) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString() =>
			$"Unable to Retrieve '{type}' with ID '{id}'.";
	}
}
