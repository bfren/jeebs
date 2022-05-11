// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Cqrs.Internals;
using Jeebs.Cqrs.Messages;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS command handler
/// </summary>
/// <typeparam name="TCommand">Command type</typeparam>
public abstract class CommandHandler<TCommand> : ICommandHandler
	where TCommand : Command
{
	/// <inheritdoc cref="ICommandHandler.HandleAsync(Command)"/>
	public abstract Task<Maybe<bool>> HandleAsync(TCommand command);

	/// <inheritdoc/>
	Task<Maybe<bool>> ICommandHandler.HandleAsync(Command command) =>
		command switch
		{
			TCommand x =>
				HandleAsync(x),

			_ =>
				F.None<bool>(new IncorrectCommandTypeMsg(typeof(TCommand), command.GetType())).AsTask()
		};
}
