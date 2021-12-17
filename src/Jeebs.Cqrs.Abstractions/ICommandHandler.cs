// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;

namespace Jeebs.Cqrs;

/// <summary>
/// Command handler
/// </summary>
/// <typeparam name="TCommand">Command type</typeparam>
/// <typeparam name="TResult">Command result</typeparam>
public interface ICommandHandler<TCommand, TResult>
{
	/// <inheritdoc cref="HandleAsync(TCommand, CancellationToken)"/>
	ValueTask<Option<TResult>> HandleAsync(TCommand query) =>
		HandleAsync(query, CancellationToken.None);

	/// <summary>
	/// Handle command
	/// </summary>
	/// <param name="query">Command object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	ValueTask<Option<TResult>> HandleAsync(TCommand query, CancellationToken cancellationToken);
}
