// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data.Querying;

/// <inheritdoc cref="IQueryAttachmentsOptions"/>
public abstract record class AttachmentsOptions : IQueryAttachmentsOptions
{
	/// <inheritdoc/>
	public IImmutableList<WpPostId> Ids { get; init; } =
		new ImmutableList<WpPostId>();
}
