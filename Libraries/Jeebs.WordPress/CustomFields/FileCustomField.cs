// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
		public override string ToString() =>
			ValueObj?.UrlPath ?? base.ToString();
	}
}
