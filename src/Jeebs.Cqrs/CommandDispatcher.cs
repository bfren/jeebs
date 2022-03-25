// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading;
using System.Threading.Tasks;
using Jeebs.Logging;
using Jeebs.Messages;

namespace Jeebs.Cqrs;

/// <inheritdoc cref="ICommandDispatcher"/>
public sealed class CommandDispatcher : ICommandDispatcher
{
	private ILog<CommandDispatcher> Log { get; init; }

	private IServiceProvider Provider { get; init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="provider"></param>
	/// <param name="log"></param>
	public CommandDispatcher(IServiceProvider provider, ILog<CommandDispatcher> log) =>
		(Provider, Log) = (provider, log);

	/// <inheritdoc/>
	public Task<Maybe<bool>> DispatchAsync<TCommand>(ICommand command) =>
		DispatchAsync(command, CancellationToken.None);

	/// <inheritdoc/>
	public Task<Maybe<bool>> DispatchAsync(ICommand command, CancellationToken cancellationToken)
	{
		// Make generic handler type
		var handlerType = typeof(CommandHandler<>).MakeGenericType(command.GetType());
		Log.Dbg("Command handler type: {Type}", handlerType);

		// Get service and handle query
		var service = Provider.GetService(handlerType);
		return service switch
		{
			ICommandHandler handler =>
				handler.HandleAsync(command, cancellationToken),

			_ =>
				F.None<bool>(new M.UnableToGetCommandHandlerMsg(command.GetType())).AsTask
		};
	}

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Unable to get command handler</summary>
		/// <param name="Value">Command Type</param>
		public sealed record class UnableToGetCommandHandlerMsg(Type Value) : WithValueMsg<Type>
		{
			/// <summary>Change value name to 'Command Type'</summary>
			public override string Name { get; init; } = "Command Type";
		}
	}
}
