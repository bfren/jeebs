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
	/// <inheritdoc cref="DispatchAsync{TQuery, TResult}(TQuery, CancellationToken)"/>
	Task<Maybe<TResult>> DispatchAsync<TQuery, TResult>(TQuery query)
		where TQuery : IQuery<TResult> =>
		DispatchAsync<TQuery, TResult>(query, CancellationToken.None);

	/// <summary>
	/// Create query of type <typeparamref name="TQuery"/> and dispatch
	/// </summary>
	/// <typeparam name="TQuery">Query type</typeparam>
	/// <typeparam name="TResult">Query result value type</typeparam>
	/// <param name="query">Query object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task<Maybe<TResult>> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
		where TQuery : IQuery<TResult>;
}
