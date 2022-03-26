// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Cqrs.Internals;

/// <summary>
/// Command handler interface which allows generic dispatching - see
/// <see cref="Dispatcher.DispatchAsync(ICommand)"/>
/// </summary>
internal interface ICommandHandler
{
	/// <inheritdoc cref="CommandHandler{TCommand}.HandleAsync(TCommand)"/>
	Task<Maybe<bool>> HandleAsync(ICommand command);
}
