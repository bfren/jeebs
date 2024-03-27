// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Cqrs.Internals;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS query handler.
/// </summary>
/// <typeparam name="TQuery">Query type.</typeparam>
/// <typeparam name="TResult">Query result value type.</typeparam>
public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TResult>
	where TQuery : Query<TResult>
{
	/// <inheritdoc cref="IQueryHandler{TResult}.HandleAsync(Query{TResult})"/>
	public abstract Task<Result<TResult>> HandleAsync(TQuery query);

	/// <inheritdoc/>
	Task<Result<TResult>> IQueryHandler<TResult>.HandleAsync(Query<TResult> query) =>
		query switch
		{
			TQuery x =>
				HandleAsync(x),

			_ =>
				R.Fail(GetType().Name, nameof(HandleAsync), "Incorrect query type.").AsTask<TResult>()
		};
}
