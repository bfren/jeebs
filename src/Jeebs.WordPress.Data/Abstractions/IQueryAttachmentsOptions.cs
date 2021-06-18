// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
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
}
