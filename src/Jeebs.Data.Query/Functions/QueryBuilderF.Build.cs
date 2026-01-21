// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryBuilderF
{
	/// <summary>
	/// Build a query and return query parts.
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="builder">Query builder.</param>
	public static Result<IQueryParts> Build<TModel>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		R.Wrap(
			new QueryBuilder()
		)
		.Map(
			x => (QueryBuilderWithFrom)builder(x),
			e => R.Fail(nameof(QueryBuilderF), nameof(Build),
				e, "Error building query."
			)
		)
		.Bind(
			x => x.Select<TModel>()
		);
}
