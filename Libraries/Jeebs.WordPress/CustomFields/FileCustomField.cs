using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// File custom field
	/// </summary>
	public abstract class FileCustomField : AttachmentCustomField
	{
		/// <inheritdoc/>
		protected FileCustomField(string key, bool isRequired = false) : base(key, isRequired) { }

		/// <summary>
		/// Return URL Path
		/// </summary>
		public override string ToString() => ValueObj?.UrlPath ?? base.ToString();
	}
}
