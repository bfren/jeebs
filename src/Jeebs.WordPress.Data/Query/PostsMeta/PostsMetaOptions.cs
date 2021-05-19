// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsMetaOptions"/>
		public sealed record PostsMetaOptions : Options<WpPostMetaId>, IQueryPostsMetaOptions
		{
			private new IQueryPostsMetaPartsBuilder Builder =>
				(IQueryPostsMetaPartsBuilder)base.Builder;

			/// <inheritdoc/>
			public WpPostId? PostId { get; init; }

			/// <inheritdoc/>
			public IImmutableList<WpPostId> PostIds { get; init; } =
				new ImmutableList<WpPostId>();

			/// <inheritdoc/>
			public string? Key { get; init; }

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			internal PostsMetaOptions(IWpDbSchema schema) : base(schema, new PostsMetaPartsBuilder(schema)) =>
				Maximum = null;

			/// <summary>
			/// Allow Builder to be injected
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			/// <param name="builder">IQueryPostsMetaPartsBuilder</param>
			internal PostsMetaOptions(IWpDbSchema schema, IQueryPostsMetaPartsBuilder builder) : base(schema, builder) { }

			/// <inheritdoc/>
			protected override Option<QueryParts> Build(Option<QueryParts> parts) =>
				base.Build(
					parts
				)
				.SwitchIf(
					_ => PostId?.Value > 0 || PostIds.Count > 0,
					ifTrue: x => Builder.AddWherePostId(x, PostId, PostIds)
				)
				.SwitchIf(
					_ => string.IsNullOrEmpty(Key),
					ifFalse: x => Builder.AddWhereKey(x, Key)
				);
		}
	}
}
