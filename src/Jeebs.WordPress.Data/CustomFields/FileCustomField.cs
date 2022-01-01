// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data;

/// <summary>
/// File Custom Field
/// </summary>
public abstract class FileCustomField : AttachmentCustomField
{
	/// <inheritdoc cref="CustomField{T}.CustomField(string, T)"/>
	protected FileCustomField(string key) : base(key) { }

	/// <inheritdoc/>
	protected override string GetValueAsString() =>
		ValueObj.UrlPath ?? base.ToString();
}
