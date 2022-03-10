// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;
using MaybeF;

namespace Jeebs.Cqrs;

/// <summary>
/// Query handler
/// </summary>
/// <typeparam name="TQuery">Query type</typeparam>
/// <typeparam name="TResult">Query result</typeparam>
public interface IQueryHandler<TQuery, TResult>
{
	/// <inheritdoc cref="HandleAsync(TQuery, CancellationToken)"/>
	Task<Maybe<TResult>> HandleAsync(TQuery query) =>
		HandleAsync(query, CancellationToken.None);

	/// <summary>
	/// Handle query
	/// </summary>
	/// <param name="query">Query object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task<Maybe<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
