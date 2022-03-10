// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;
using MaybeF;

namespace Jeebs.Cqrs;

/// <summary>
/// Command dispatcher
/// </summary>
public interface ICommandDispatcher
{
	/// <inheritdoc cref="DispatchAsync{TCommand, TResult}(TCommand, CancellationToken)"/>
	ValueTask<Maybe<TResult>> DispatchAsync<TCommand, TResult>(TCommand query) =>
		DispatchAsync<TCommand, TResult>(query, CancellationToken.None);

	/// <summary>
	/// Create command of type <typeparamref name="TCommand"/> and dispatch
	/// </summary>
	/// <typeparam name="TCommand">Command type</typeparam>
	/// <typeparam name="TResult">Command result</typeparam>
	/// <param name="query">Command object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	ValueTask<Maybe<TResult>> DispatchAsync<TCommand, TResult>(TCommand query, CancellationToken cancellationToken);
}
