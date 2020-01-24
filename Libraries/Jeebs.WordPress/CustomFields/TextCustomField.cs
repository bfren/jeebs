using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Simple text value custom field
	/// </summary>
	public abstract class TextCustomField : CustomField<string>
	{
		/// <summary>
		/// Custom Field value
		/// </summary>
		public override string Val => value;

		/// <summary>
		/// Pass post_meta key to parent
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		protected TextCustomField(string key) : base(key) { }
	}
}
