using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

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
