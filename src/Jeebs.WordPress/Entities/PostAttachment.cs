// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;
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
				PhpF.Deserialise(info)
			)
			.Unwrap(
				JsonF.Empty
			);

		init =>
			info = value;
	}

	/// <summary>
	/// PHP serialised info
	/// </summary>
	private string info = string.Empty;

	/// <inheritdoc/>
	public string GetFilePath(string wpUploadsPath) =>
		wpUploadsPath.EndWith('/') + UrlPath.TrimStart('/');
}
