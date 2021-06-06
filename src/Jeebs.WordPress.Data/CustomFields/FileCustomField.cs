// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		protected FileCustomField(string key) : base(key) { }
	}
}
