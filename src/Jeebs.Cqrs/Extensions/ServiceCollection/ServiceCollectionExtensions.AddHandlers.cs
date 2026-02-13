// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using Jeebs.Functions;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Cqrs;

public static partial class ServiceCollectionExtensions
{
	/// <summary>
	/// Add handlers to <paramref name="services"/>.
	/// </summary>
	/// <param name="services">IServiceCollection.</param>
	/// <param name="handlerType">The handler base type.</param>
	/// <param name="genericArguments">The number of generic arguments the handler base type has.</param>
	internal static void AddHandlers(IServiceCollection services, Type handlerType, int genericArguments)
	{
		// Get all matching handlers
		var handlers = from t in TypeF.AllTypes.Value
					   where t.BaseType?.GenericTypeArguments.Length == genericArguments
					   && t.BaseType.GetGenericTypeDefinition().IsAssignableFrom(handlerType)
					   select t;

		// Add each handler as itself, and as the base type so the dispatcher can find it
		foreach (var q in handlers)
		{
			if (q.BaseType is null)
			{
				continue;
			}

			_ = services.AddTransient(q);
			_ = services.AddTransient(q.BaseType, p => p.GetRequiredService(q));
		}
	}
}
