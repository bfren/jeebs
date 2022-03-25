// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;

namespace Jeebs.Cqrs.Internals;

/// <summary>
/// Query handler interface which allows generic dispatching - see
/// <see cref="Dispatcher.DispatchAsync{TResult}(IQuery{TResult}, CancellationToken)"/>
/// </summary>
/// <typeparam name="TResult">Query result value type</typeparam>
internal interface IQueryHandler<TResult>
{
	/// <inheritdoc cref="QueryHandler{TQuery, TResult}.HandleAsync(TQuery, CancellationToken)"/>
	Task<Maybe<TResult>> HandleAsync(IQuery<TResult> query, CancellationToken cancellationToken);
}
