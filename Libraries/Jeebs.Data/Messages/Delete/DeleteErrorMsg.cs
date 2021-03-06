// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jm.Data
{
	/// <summary>
	/// Message about an error that has occurred during a Delete operation
	/// </summary>
	public class DeleteErrorMsg : DeleteMsg
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteErrorMsg(Type type, long id) : base(type, id) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString() =>
			$"Unable to Delete '{type}' with ID '{id}'.";
	}
}
