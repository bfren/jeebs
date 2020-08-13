using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

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
		public override string ToString()
			=> $"Unable to Delete '{type}' with ID '{id}'.";
	}
}
