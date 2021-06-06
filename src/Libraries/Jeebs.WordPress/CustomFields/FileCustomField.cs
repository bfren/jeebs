// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
