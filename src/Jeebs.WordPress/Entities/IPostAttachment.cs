// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using StrongId;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Attachment entity
/// </summary>
public interface IPostAttachment : IWithId<StrongIds.WpPostId>
{
	/// <summary>
	/// Attachment description
	/// </summary>
	string Description { get; init; }

	/// <summary>
	/// MetaDictionary
	/// </summary>
	MetaDictionary Meta { get; init; }

	/// <summary>
	/// UrlPath
	/// </summary>
	string UrlPath { get; init; }

	/// <summary>
	/// Additional information about the file (encoded as JSON)
	/// </summary>
	string Info { get; init; }

	/// <summary>
	/// Get the filesystem path to this attachment
	/// </summary>
	/// <param name="wpUploadsPath">Filesystem path to wp-uploads directory</param>
	string GetFilePath(string wpUploadsPath);
}
