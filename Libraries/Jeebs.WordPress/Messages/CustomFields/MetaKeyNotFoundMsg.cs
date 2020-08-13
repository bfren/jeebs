using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.WordPress.CustomFields
{
	/// <summary>
	/// Meta key not found when hydrating a custom field
	/// </summary>
	public class MetaKeyNotFoundMsg : WithValueMsg<string>
	{
		internal MetaKeyNotFoundMsg(string key) : base(key) { }

		/// <summary>
		/// Return error message
		/// </summary>
		public override string ToString()
			=> $"Key not found in meta dictionary: '{Value}'.";
	}
}
