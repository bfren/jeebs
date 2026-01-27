// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Post Attachment.
/// </summary>
public abstract record class PostAttachment : WpPostEntity, IPostAttachment
{
	/// <inheritdoc/>
	public string Description
	{
		get =>
			Excerpt;

		init =>
			Excerpt = value;
	}

	/// <inheritdoc/>
	public MetaDictionary Meta { get; init; } = [];

	/// <inheritdoc/>
	public string UrlPath { get; init; } = string.Empty;

	/// <inheritdoc/>
	public string Info
	{
		get =>
			JsonF.Serialise(
				PhpF.Deserialise(field)
			)
			.Unwrap(
				_ => JsonF.Empty
			);

		init =>
			field = value;
	} = string.Empty;

	/// <inheritdoc/>
	public string GetFilePath(string wpUploadsPath) =>
		wpUploadsPath.EndWith('/') + UrlPath.TrimStart('/');
}
