// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Querying;
using Jeebs.WordPress.Enums;
using static F.OptionF;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Taxonomy
		/// </summary>
		/// <typeparam name="TModel">Term type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		public Task<Option<List<TModel>>> QueryTaxonomyAsync<TModel>(Action<QueryTaxonomy.Options>? modify = null) =>
			Return(modify)
				.Bind(
					GetTaxonomyQuery<TModel>
				)
				.BindAsync(
					x => x.ExecuteQueryAsync()
				);

		/// <summary>
		/// Simple query - get terms from a particular taxonomy, with optional sorting
		/// </summary>
		/// <typeparam name="TModel">Term type</typeparam>
		/// <param name="taxonomy">The taxonomy to get</param>
		/// <param name="all">If true, will return all terms (even if the count is 0)</param>
		/// <param name="sort">[Optional] Sort columns</param>
		public Task<Option<List<TModel>>> QueryTaxonomyAsync<TModel>(
			Taxonomy taxonomy,
			bool all = true,
			params (string column, SortOrder order)[] sort
		) =>
			QueryTaxonomyAsync<TModel>(opt =>
			{
				opt.Taxonomy = taxonomy;
				opt.Sort = sort;

				if (all)
				{
					opt.CountAtLeast = null;
				}
			});

		/// <summary>
		/// Get query object
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		private Option<IQuery<TModel>> GetTaxonomyQuery<TModel>(Action<QueryTaxonomy.Options>? modify = null) =>
			Return(
				() => StartNewQuery()
						.WithModel<TModel>()
						.WithOptions(modify)
						.WithParts(new QueryTaxonomy.Builder<TModel>(db))
						.GetQuery(),
				e => new Msg.GetTaxonomyQueryExceptionMsg(e)
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get taxonomy query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetTaxonomyQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
