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
	/// <inheritdoc cref="DispatchAsync(ICommand, CancellationToken)"/>
	Task<Maybe<bool>> DispatchAsync(ICommand command);

	/// <summary>
	/// Create command of type <typeparamref name="TCommand"/> and dispatch
	/// </summary>
	/// <param name="command">Command object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task<Maybe<bool>> DispatchAsync(ICommand command, CancellationToken cancellationToken);
}
