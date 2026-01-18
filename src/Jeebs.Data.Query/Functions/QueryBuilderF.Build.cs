// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryBuilderF
{
	/// <summary>
	/// Build a query and return query parts.
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="builder">Query builder.</param>
	public static Maybe<IQueryParts> Build<TModel>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		F.Some(
			new QueryBuilder()
		)
		.Map(
			x => (QueryBuilderWithFrom)builder(x),
			e => new M.QueryBuilderExceptionMsg(e)
		)
		.Bind(
			x => x.Select<TModel>()
		);

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Error while building query</summary>
		/// <param name="Value">Exception object.</param>
		public sealed record class QueryBuilderExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
