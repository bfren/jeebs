// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading;
using System.Threading.Tasks;
using Jeebs.Messages;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS command handler
/// </summary>
/// <typeparam name="TCommand">Command type</typeparam>
public abstract class CommandHandler<TCommand> : ICommandHandler
	where TCommand : ICommand
{
	/// <summary>
	/// Handle a command
	/// </summary>
	/// <param name="command">Command object</param>
	/// <param name="cancellationToken">Cancellation token</param>
	public abstract Task<Maybe<bool>> HandleAsync(TCommand command, CancellationToken cancellationToken);

	/// <inheritdoc cref="HandleAsync(TCommand, CancellationToken)"/>
	public Task<Maybe<bool>> HandleAsync(TCommand command) =>
		HandleAsync(command, CancellationToken.None);

	/// <inheritdoc/>
	Task<Maybe<bool>> ICommandHandler.HandleAsync(ICommand command, CancellationToken cancellationToken) =>
		command switch
		{
			TCommand x =>
				HandleAsync(x, cancellationToken),

			_ =>
				F.None<bool>(new M.IncorrectCommandTypeMsg(typeof(TCommand), command.GetType())).AsTask
		};

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>The query is an incorrect type for the handler</summary>
		/// <param name="ExpectedType">Expected query type</param>
		/// <param name="ActualType">Actual query type</param>
		public sealed record class IncorrectCommandTypeMsg(Type ExpectedType, Type ActualType) : Msg;
	}
}
