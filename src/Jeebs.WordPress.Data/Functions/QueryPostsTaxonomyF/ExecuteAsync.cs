// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsTaxonomyF
	{
		/// <summary>
		/// Execute Posts Taxonomy query
		/// </summary>
		/// <typeparam name="TModel">Return Model type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="opt">Function to return query options</param>
		public static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(
			IWpDb db,
			Query.GetPostsTaxonomyOptions opt
		)
			where TModel : IWithId<WpTermId>
		{
			return
				Return(
					() => opt(new Query.PostsTaxonomyOptions(db)),
					e => new Msg.ErrorGettingQueryPostsTaxonomyOptionsMsg(e)
				)
				.Bind(
					x => x.GetParts<TModel>()
				)
				.BindAsync(
					x => db.Query.QueryAsync<TModel>(x)
				);
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts taxonomy query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryPostsTaxonomyOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
