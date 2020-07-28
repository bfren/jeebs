using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.WordPress.CustomField.Hydrate
{
	public class MetaKeyNotFoundMsg : WithStringMsg
	{
		internal MetaKeyNotFoundMsg(string key) : base(key) { }

		public override string ToString()
			=> $"Key not found in meta dictionary: '{Value}'.";
	}
}
