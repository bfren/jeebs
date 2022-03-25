// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS query dispatcher
/// </summary>
public interface IQueryDispatcher
{
	/// <inheritdoc cref="DispatchAsync{TResult}(IQuery{TResult}, CancellationToken)"/>
	Task<Maybe<TResult>> DispatchAsync<TResult>(IQuery<TResult> query);

	/// <summary>
	/// Call <see cref="QueryHandler{TQuery, TResult}.HandleAsync(TQuery, CancellationToken)"/> for <paramref name="query"/>
	/// </summary>
	/// <typeparam name="TResult">Query result value type</typeparam>
	/// <param name="query">Query object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task<Maybe<TResult>> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}
