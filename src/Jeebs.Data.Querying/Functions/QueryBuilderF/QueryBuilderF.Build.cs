// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Data.Querying;
using static F.OptionF;

namespace F.DataF;

public static partial class QueryBuilderF
{
	/// <summary>
	/// Build a query and return query parts
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="builder">Query builder</param>
	public static Option<IQueryParts> Build<TModel>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		Some(
			new QueryBuilder()
		)
		.Map(
			x => (QueryBuilderWithFrom)builder(x),
			e => new Msg.QueryBuilderExceptionMsg(e)
		)
		.Bind(
			x => x.Select<TModel>()
		);

	/// <summary>Messages</summary>
	public static class Msg
	{
		/// <summary>Error while building query</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class QueryBuilderExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
	}
}
