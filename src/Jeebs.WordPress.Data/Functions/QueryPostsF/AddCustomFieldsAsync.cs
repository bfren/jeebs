// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Add custom fields to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="query">IWpDbQuery</param>
		/// <param name="posts">Posts</param>
		internal static Task<Option<TList>> AddCustomFieldsAsync<TList, TModel>(IWpDbQuery query, TList posts)
			where TList : IEnumerable<TModel>
			where TModel : IWithId =>
			Return(posts).AsTask;
	}
}
