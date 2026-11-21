// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Auth.Data;

/// <summary>
/// Builds the Auth database client so user lookups are never cached - a cached lookup would let an
/// old password, or a disabled user, keep signing in until the entry expired.
/// </summary>
/// <param name="provider">The app's service provider, used for everything else.</param>
internal sealed class UncachedServiceProvider(IServiceProvider provider) : IServiceProvider
{
	/// <summary>
	/// Returns <see cref="NullCacheService"/> for <see cref="ICacheService"/>, a new instance of the app's
	/// <see cref="IAdapter"/> type built with no cache for <see cref="IAdapter"/>, and otherwise the
	/// app's service.
	/// </summary>
	/// <param name="serviceType">Service type.</param>
	public object? GetService(Type serviceType)
	{
		if (serviceType == typeof(ICacheService))
		{
			return new NullCacheService();
		}

		if (serviceType == typeof(IAdapter) && provider.GetService<IAdapter>() is IAdapter adapter)
		{
			return ActivatorUtilities.CreateInstance(this, adapter.GetType());
		}

		return provider.GetService(serviceType);
	}
}
