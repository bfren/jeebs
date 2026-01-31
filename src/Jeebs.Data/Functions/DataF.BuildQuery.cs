// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Query;

namespace Jeebs.Data.Functions;

public static partial class DataF
{
	/// <summary>
	/// Build a query and return query parts.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="builder">Query builder.</param>
	public static Result<IQueryParts> BuildQuery<TModel>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		R.Wrap(
			new QueryBuilder()
		)
		.Map(
			x => (QueryBuilderWithFrom)builder(x),
			e => R.Fail(e).Msg("Error building query.").Ctx(nameof(DataF), nameof(BuildQuery))
		)
		.Bind(
			x => x.Select<TModel>()
		);
}
