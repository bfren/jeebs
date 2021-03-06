// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jm.Data
{
	/// <summary>
	/// Message about an error that has occurred during an update operation
	/// </summary>
	public class UpdateErrorMsg : UpdateMsg
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public UpdateErrorMsg(Type type, long id) : base(type, id) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString() =>
			$"Unable to update '{type}' with ID '{id}'.";
	}
}
