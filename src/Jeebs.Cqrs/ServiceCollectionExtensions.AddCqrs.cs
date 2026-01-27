// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Cqrs;

public static partial class ServiceCollectionExtensions
{
	/// <summary>
	/// Register all <see cref="QueryHandler{TQuery, TResult}"/> and <see cref="CommandHandler{TCommand}"/>
	/// types to <paramref name="services"/>.
	/// </summary>
	/// <param name="services">IServiceCollection.</param>
	/// <returns>Modified IServiceCollection.</returns>
	public static IServiceCollection AddCqrs(this IServiceCollection services)
	{
		// Add dispatcher
		_ = services.AddSingleton<IDispatcher, Dispatcher>();

		// Add handlers
		AddHandlers(services, typeof(CommandHandler<>), 1);
		AddHandlers(services, typeof(QueryHandler<,>), 2);

		// Return
		return services;
	}
}
