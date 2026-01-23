// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Cqrs.Internals;
using Jeebs.Logging;

namespace Jeebs.Cqrs;

/// <inheritdoc cref="IDispatcher"/>
public sealed class Dispatcher : IDispatcher
{
	private ILog<Dispatcher> Log { get; init; }

	private IServiceProvider Provider { get; init; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="provider">IServiceProvider.</param>
	/// <param name="log">ILog.</param>
	public Dispatcher(IServiceProvider provider, ILog<Dispatcher> log) =>
		(Provider, Log) = (provider, log);

	/// <inheritdoc/>
	public Task<Result<bool>> SendAsync(Command command)
	{
		var service = GetHandlerService(typeof(CommandHandler<>), command.GetType());
		return service switch
		{
			ICommandHandler handler =>
				handler.HandleAsync(command),

			_ =>
				R.Fail("Unable to get command handler {Type}.", new { Type = command.GetType().Name })
					.Ctx(nameof(Dispatcher), nameof(SendAsync))
					.AsTask<bool>()
		};
	}

	/// <inheritdoc/>
	public Task<Result<bool>> SendAsync<T>()
		where T : Command, new() =>
		SendAsync(new T());

	/// <inheritdoc/>
	public Task<Result<T>> SendAsync<T>(Query<T> query)
	{
		var service = GetHandlerService(typeof(QueryHandler<,>), query.GetType(), typeof(T));
		return service switch
		{
			IQueryHandler<T> handler =>
				handler.HandleAsync(query),

			_ =>
				R.Fail("Unable to get query handler {Type}.", new { Type = query.GetType().Name })
					.Ctx(nameof(Dispatcher), nameof(SendAsync))
					.AsTask<T>()
		};
	}

	/// <summary>
	/// Use <see cref="Provider"/> to get a service of type <paramref name="genericType"/>.
	/// </summary>
	/// <param name="genericType">The generic handler type.</param>
	/// <param name="typeArguments">Specific type arguments used to create the handler type.</param>
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
