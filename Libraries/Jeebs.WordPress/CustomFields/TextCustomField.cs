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
		/// Pass post_meta key to parent
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		protected TextCustomField(string key) : base(key, string.Empty) { }

		/// <summary>
		/// Return custom field value
		/// </summary>
		/// <returns>Custom field value</returns>
		public override string ToString() => Value ?? base.ToString();
	}
}
