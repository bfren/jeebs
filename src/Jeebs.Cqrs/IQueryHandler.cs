// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS query handler
/// </summary>
/// <typeparam name="TQuery">Query type</typeparam>
/// <typeparam name="TResult">Query result value type</typeparam>
public interface IQueryHandler<TQuery, TResult>
	where TQuery : IQuery<TResult>
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
