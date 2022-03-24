// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS command dispatcher
/// </summary>
public interface ICommandDispatcher
{
	/// <inheritdoc cref="DispatchAsync{TCommand}(TCommand, CancellationToken)"/>
	Task<Maybe<bool>> DispatchAsync<TCommand>(TCommand query)
		where TCommand : ICommand =>
		DispatchAsync(query, CancellationToken.None);

	/// <summary>
	/// Create command of type <typeparamref name="TCommand"/> and dispatch
	/// </summary>
	/// <typeparam name="TCommand">Command type</typeparam>
	/// <param name="query">Command object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task<Maybe<bool>> DispatchAsync<TCommand>(TCommand query, CancellationToken cancellationToken)
		where TCommand : ICommand;
}
