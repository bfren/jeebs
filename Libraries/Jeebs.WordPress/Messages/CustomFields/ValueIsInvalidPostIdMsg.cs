using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.WordPress.CustomFields
{
	/// <summary>
	/// ValueStr is an invalid Post ID when hydrating a custom field
	/// </summary>
	public class ValueIsInvalidPostIdMsg : WithStringMsg
	{
		internal ValueIsInvalidPostIdMsg(string value) : base(value) { }

		/// <summary>
		/// Return error message
		/// </summary>
		public override string ToString()
			=> $"'{Value}' is not a valid Post ID.";
	}
}
