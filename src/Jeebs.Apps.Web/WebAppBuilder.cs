// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps.Web;

/// <summary>
/// Create a new <see cref="WebApplication"/> with default Jeebs configuration.
/// </summary>
internal static class WebAppBuilder
{
	/// <summary>
	/// Build a <see cref="WebApplication"/> with default configuration and then run it.
	/// </summary>
	/// <inheritdoc cref="Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	internal static void Run<T>(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices)
		where T : WebApp, new() =>
		Create<T>(args, configureServices).app.Run();

	/// <inheritdoc cref="WebAppBuilder"/>
	/// <remarks>
	///   - Default configuration is loaded using <see cref="Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(string[])"/><br/>
	///   - Jeebs configuration is loaded from JSON files and (optionally) Azure Key Vault<br/>
	///   - Options classes are bound to configuration<br/>
	///   - Application services are registered with the DI container<br/>
	///   - Custom services are registered using <paramref name="configureServices"/><br/>
	///   - Logging is enabled using Serilog<br/>
	///   - <see cref="WebApplication"/> is built<br/>
	///   - <see cref="WebApplication"/> is configured
	///   - <see cref="App.Ready(IServiceProvider, ILog)"/> is run
	/// </remarks>
	/// <typeparam name="T">App type.</typeparam>
	/// <param name="args">Command-line arguments.</param>
	/// <param name="configureServices">Add additional services.</param>
	internal static (WebApplication app, ILog<T> log) Create<T>(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices)
		where T : WebApp, new()
	{
		// Create app and builder
		var app = new T();
		var builder = WebApplication.CreateBuilder(args);

		// Configure and build host
		_ = builder.Host.ConfigureHostBuilder(app, configureServices);

		var webApplication = builder.Build();

		// Configure application
		app.Configure(webApplication);

		// Get log service
		var log = webApplication.Services.GetRequiredService<ILog<T>>();

		// App is ready
		app.Ready(webApplication.Services, log);

		// Return host and log
		return (webApplication, log);
	}
}
