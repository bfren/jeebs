// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Query.Options;

/// <summary>
/// Query Attachments Options.
/// </summary>
public interface IQueryAttachmentsOptions
{
	/// <summary>
	/// Attachment IDs.
	/// </summary>
	IImmutableList<WpPostId> Ids { get; init; }
}
