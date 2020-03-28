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
		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="key">Meta key</param>
		/// <param name="isRequired">[Optional] Whether or not this custom field is required (default: false)</param>
		protected FileCustomField(string key, bool isRequired = false) : base(key, isRequired) { }

		/// <summary>
		/// Return URL Path
		/// </summary>
		public override string ToString() => ValueObj?.UrlPath ?? base.ToString();
	}
}
