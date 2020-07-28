using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.WordPress.CustomField.Hydrate
{
	public class ValueIsInvalidPostIdMsg : WithStringMsg
	{
		internal ValueIsInvalidPostIdMsg(string value) : base(value) { }

		public override string ToString()
			=> $"'{Value}' is not a valid Post ID.";
	}
}
