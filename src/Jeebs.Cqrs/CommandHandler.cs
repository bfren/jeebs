// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Cqrs.Internals;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS command handler.
/// </summary>
/// <typeparam name="T">Command type.</typeparam>
public abstract class CommandHandler<T> : ICommandHandler
	where T : Command
{
	/// <inheritdoc cref="ICommandHandler.HandleAsync(Command)"/>
	public abstract Task<Result<bool>> HandleAsync(T command);

	/// <inheritdoc/>
	Task<Result<bool>> ICommandHandler.HandleAsync(Command command) =>
		command switch
		{
			T x =>
				HandleAsync(x),

			_ =>
				R.Fail(GetType().Name, nameof(HandleAsync), "Incorrect command type.").AsTask<bool>()
		};
}
