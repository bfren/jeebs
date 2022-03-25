// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS dispatcher
/// </summary>
public interface IDispatcher
{
	/// <inheritdoc cref="DispatchAsync(ICommand, CancellationToken)"/>
	Task<Maybe<bool>> DispatchAsync(ICommand command);

	/// <summary>
	/// Call <see cref="CommandHandler{TCommand}.HandleAsync(TCommand, CancellationToken)"/> for <paramref name="command"/>
	/// </summary>
	/// <param name="command">Command object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task<Maybe<bool>> DispatchAsync(ICommand command, CancellationToken cancellationToken);

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
