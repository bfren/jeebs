// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Functions;
using Jeebs.Services.Notify;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Services;

/// <summary>
/// <see cref="IServiceCollection"/> Extensions - AddDrivers.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Add <see cref="IDriver{TConfig}"/> and <see cref="IDriverArgs{TConfig}"/> implementations to the <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="this">IServiceCollection</param>
	public static IServiceCollection AddDrivers(this IServiceCollection @this)
	{
		// Get drivers and add them to the service collection
		var foundListeners = false;
		foreach (var t in GetImplementations(typeof(IDriver<>)))
		{
			// Register type
			_ = @this.AddSingleton(t);

			// Add as a listener
			if (t.Implements<INotificationListener>())
			{
				_ = @this.AddTransient(typeof(INotificationListener), t);
				foundListeners = true;
			}
		}

		// If listeners were added, add the notifier
		if (foundListeners)
		{
			_ = @this.AddSingleton<INotifier, Notifier>();
		}

		// Get driver args and add them to the service collection
		foreach (var t in GetImplementations(typeof(IDriverArgs<>)))
		{
			_ = @this.AddTransient(t);
		}

		// Return service collection
		return @this;

		// Get implementations of an interface with a generic parameter
		static IEnumerable<Type> GetImplementations(Type typeWithGenericParam) =>
			from t in TypeF.AllTypes.Value
			where t.Implements(typeWithGenericParam) && !t.IsAbstract && !t.IsInterface
			select t;
	}
}
