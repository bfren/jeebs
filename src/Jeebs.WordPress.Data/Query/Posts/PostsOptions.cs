// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Querying;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsOptions"/>
		public sealed record class PostsOptions : Querying.PostsOptions
		{
			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			internal PostsOptions(IWpDbSchema schema) : base(schema, new PostsPartsBuilder(schema)) { }

			/// <summary>
			/// Allow Builder to be injected
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			/// <param name="builder">IQueryPostsPartsBuilder</param>
			internal PostsOptions(IWpDbSchema schema, IQueryPostsPartsBuilder builder) : base(schema, builder) { }

			/// <inheritdoc/>
			protected override Option<QueryParts> Build(Option<QueryParts> parts) =>
				base.Build(
					parts
				)
				.Bind(
					x => Builder.AddWhereType(x, Type)
				)
				.Bind(
					x => Builder.AddWhereStatus(x, Status)
				)
				.SwitchIf(
					_ => string.IsNullOrEmpty(SearchText),
					ifFalse: x => Builder.AddWhereSearch(x, SearchFields, SearchComparison, SearchText)
				)
				.SwitchIf(
					_ => From is not null,
					ifTrue: x => Builder.AddWherePublishedFrom(x, From)
				)
				.SwitchIf(
					_ => To is not null,
					ifTrue: x => Builder.AddWherePublishedTo(x, To)
				)
				.SwitchIf(
					_ => ParentId is not null,
					ifTrue: x => Builder.AddWhereParentId(x, ParentId)
				)
				.SwitchIf(
					_ => Taxonomies.Count > 0,
					ifTrue: x => Builder.AddWhereTaxonomies(x, Taxonomies)
				)
				.SwitchIf(
					_ => CustomFields.Count > 0,
					ifTrue: x => Builder.AddWhereCustomFields(x, CustomFields)
				);
		}
	}
}
