// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data;

/// <summary>
/// File Custom Field
/// </summary>
public abstract class FileCustomField : AttachmentCustomField
{
	/// <summary>
	/// Create object with specified meta key
	/// </summary>
	/// <param name="key">Meta key (for post_meta table)</param>
	protected FileCustomField(string key) : base(key) { }

	/// <inheritdoc/>
	protected override string GetValueAsString() =>
		ValueObj.UrlPath ?? base.ToString();
}
