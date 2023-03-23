// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Cqrs.Internals;

/// <summary>
/// Command handler interface which allows generic dispatching - see
/// <see cref="Dispatcher.SendAsync(Command)"/>
/// </summary>
internal interface ICommandHandler
{
	/// <summary>
	/// Handle a command
	/// </summary>
	/// <param name="command">Command object</param>
	Task<Maybe<bool>> HandleAsync(Command command);
}
