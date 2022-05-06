// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Cqrs.Internals;
using Jeebs.Cqrs.Messages;
using Jeebs.Logging;

namespace Jeebs.Cqrs;

/// <inheritdoc cref="IDispatcher"/>
public sealed class Dispatcher : IDispatcher
{
	private ILog<Dispatcher> Log { get; init; }

	private IServiceProvider Provider { get; init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="provider"></param>
	/// <param name="log"></param>
	public Dispatcher(IServiceProvider provider, ILog<Dispatcher> log) =>
		(Provider, Log) = (provider, log);

	/// <inheritdoc/>
	public Task<Maybe<bool>> DispatchAsync(Command command)
	{
		var service = GetHandlerService(typeof(CommandHandler<>), command.GetType());
		return service switch
		{
			ICommandHandler handler =>
				handler.HandleAsync(command),

			_ =>
				F.None<bool>(new UnableToGetCommandHandlerMsg(command.GetType())).AsTask
		};
	}

	/// <inheritdoc/>
	public Task<Maybe<TResult>> DispatchAsync<TResult>(Query<TResult> query)
	{
		var service = GetHandlerService(typeof(QueryHandler<,>), query.GetType(), typeof(TResult));
		return service switch
		{
			IQueryHandler<TResult> handler =>
				handler.HandleAsync(query),

			_ =>
				F.None<TResult>(new UnableToGetQueryHandlerMsg(query.GetType())).AsTask
		};
	}

	/// <summary>
	/// Use <see cref="Provider"/> to get a service of type <paramref name="genericType"/>
	/// </summary>
	/// <param name="genericType">The generic handler type</param>
	/// <param name="typeArguments">Specific type arguments used to create the handler type</param>
	internal object? GetHandlerService(Type genericType, params Type[] typeArguments)
	{
		// Make generic handler type
		var handlerType = genericType.MakeGenericType(typeArguments);

		// Get service
		var service = Provider.GetService(handlerType);
		if (service is not null)
		{
			Log.Vrb("Handler: {Type}", service.GetType());
		}

		return service;
	}
}
