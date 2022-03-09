// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress;

/// <summary>
/// Query Attachments Options
/// </summary>
public interface IQueryAttachmentsOptions
{
	/// <summary>
	/// Attachment IDs
	/// </summary>
	IImmutableList<WpPostId> Ids { get; init; }
}
