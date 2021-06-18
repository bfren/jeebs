// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryAttachmentsOptions"/>
		public sealed record AttachmentsOptions : IQueryAttachmentsOptions
		{
			/// <inheritdoc/>
			public IImmutableList<WpPostId> Ids { get; init; } =
				new ImmutableList<WpPostId>();
		}
	}
}
