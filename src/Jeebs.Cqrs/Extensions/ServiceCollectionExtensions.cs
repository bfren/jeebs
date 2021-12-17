// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Cqrs;

/// <summary>
/// ServiceCollection extensions: AddCqrs
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Register all <see cref="IQueryHandler{TQuery, TResult}"/> and <see cref="ICommandHandler{TCommand, TResult}"/>
	/// types to <paramref name="services"/>
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	public static IServiceCollection AddCqrs(this IServiceCollection services)
	{
		// Add dispatchers
		services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
		services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

		// Add commands
		services.Scan(selector => selector
			.FromEntryAssembly()
			.AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<,>)))
			.AsSelfWithInterfaces()
			.WithSingletonLifetime()
		);

		// Add queries
		services.Scan(selector => selector
			.FromEntryAssembly()
			.AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>)))
			.AsSelfWithInterfaces()
			.WithSingletonLifetime()
		);

		// Return
		return services;
	}
}
