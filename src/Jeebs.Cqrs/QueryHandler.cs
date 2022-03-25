// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
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
	where TQuery : IQuery<TResult>
{
	/// <summary>
	/// Handle a query object
	/// </summary>
	/// <param name="query">Query object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	public abstract Task<Maybe<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);

	/// <inheritdoc cref="HandleAsync(TQuery, CancellationToken)"/>
	public Task<Maybe<TResult>> HandleAsync(TQuery query) =>
		HandleAsync(query, CancellationToken.None);

	/// <inheritdoc/>
	Task<Maybe<TResult>> IQueryHandler<TResult>.HandleAsync(IQuery<TResult> query, CancellationToken cancellationToken) =>
		query switch
		{
			TQuery x =>
				HandleAsync(x, cancellationToken),

			_ =>
				F.None<TResult>(new IncorrectQueryTypeMsg(typeof(TQuery), query.GetType())).AsTask
		};
}
