using System;
using System.Collections.Generic;
using System.Text;

namespace Jm
{
	/// <summary>
	/// Special case: String value
	/// </summary>
	public class WithStringMsg : WithValueMsg<string>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">String</param>
		public WithStringMsg(string value) : base(value) { }
	}
}
