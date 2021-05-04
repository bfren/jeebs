// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// File Custom Field
	/// </summary>
	public abstract record FileCustomField : AttachmentCustomField
	{
		/// <summary>
		/// Create object with specified meta key
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		/// <param name="isRequired">Whether or not this custom field is required</param>
		protected FileCustomField(string key, bool isRequired = false) : base(key, isRequired) { }
	}
}
