// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Process a list of posts, adding meta / taxonomies / custom fields as required
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="posts">Posts</param>
		/// <param name="filters">Optional content filters</param>
		internal static Task<Option<TList>> Process<TList, TModel>(IWpDb db, IUnitOfWork w, TList posts, params IContentFilter[] filters)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
		{
			return
				Return(
					posts
				)
				.BindAsync(
					x => AddMetaAsync<TList, TModel>(db, w, x)
				)
				.BindAsync(
					x => AddCustomFieldsAsync<TList, TModel>(db, w, x)
				)
				.BindAsync(
					x => AddTaxonomiesAsync<TList, TModel>(db, w, x)
				)
				.SwitchIfAsync(
					_ => filters.Length > 0,
					ifTrue: x => ApplyContentFilters<TList, TModel>(x, filters)
				);
		}
	}
}
