// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Cqrs.Internals;
using Jeebs.Cqrs.Messages;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS query handler
/// </summary>
/// <typeparam name="TQuery">Query type</typeparam>
/// <typeparam name="TResult">Query result value type</typeparam>
public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TResult>
	where TQuery : Query<TResult>
{
	/// <inheritdoc cref="IQueryHandler{TResult}.HandleAsync(Query{TResult})"/>
	public abstract Task<Maybe<TResult>> HandleAsync(TQuery query);

	/// <inheritdoc/>
	Task<Maybe<TResult>> IQueryHandler<TResult>.HandleAsync(Query<TResult> query) =>
		query switch
		{
			TQuery x =>
				HandleAsync(x),

			_ =>
				F.None<TResult>(new IncorrectQueryTypeMsg(typeof(TQuery), query.GetType())).AsTask
		};
}
