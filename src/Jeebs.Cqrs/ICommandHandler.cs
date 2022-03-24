// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS command handler
/// </summary>
/// <typeparam name="TCommand">Command type</typeparam>
public interface ICommandHandler<TCommand>
	where TCommand : ICommand
{
	/// <inheritdoc cref="HandleAsync(TCommand, CancellationToken)"/>
	Task<Maybe<bool>> HandleAsync(TCommand query) =>
		HandleAsync(query, CancellationToken.None);

	/// <summary>
	/// Handle command
	/// </summary>
	/// <param name="query">Command object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task<Maybe<bool>> HandleAsync(TCommand query, CancellationToken cancellationToken);
}
