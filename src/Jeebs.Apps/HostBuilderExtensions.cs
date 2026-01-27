// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jeebs.Apps;

/// <summary>
/// <see cref="IHostBuilder"/> extensions.
/// </summary>
public static class HostBuilderExtensions
{
	/// <summary>
	/// Default Jeebs HostBuilder configuration.
	/// </summary>
	/// <param name="this">IHostBuilder.</param>
	/// <param name="app">App.</param>
	/// <param name="configureServices">Add additional services.</param>
	/// <returns>Configured IHostBuilder.</returns>
	public static IHostBuilder ConfigureHostBuilder(this IHostBuilder @this, App app, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		@this
			.ConfigureHostConfiguration(app.ConfigureHost)
			.ConfigureAppConfiguration(app.ConfigureApp)
			.ConfigureServices(app.ConfigureServices)
			.ConfigureServices(configureServices)
			.UseSerilog(app.ConfigureSerilog);
}
