// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jeebs.Apps;

/// <summary>
/// Uses .NET 6 hosting model, with default options.
/// </summary>
public static class Host
{
	#region Run

	/// <inheritdoc cref="Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static void Run(string[] args) =>
		Create(args).app.Run();

	/// <inheritdoc cref="Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static void Run(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		Create(args, configureServices).app.Run();

	/// <inheritdoc cref="Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static void Run<T>(string[] args)
		where T : App, new() =>
		Create<T>(args).app.Run();

	/// <summary>
	/// Build an <see cref="IHost"/> with default Jeebs configuration and then run it.
	/// </summary>
	/// <inheritdoc cref="CreateBuilder{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	internal static void Run<T>(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices)
		where T : App, new() =>
		Create<T>(args, configureServices).app.Run();

	#endregion Run

	#region Create

	/// <inheritdoc cref="Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (IHost app, ILog<App> log) Create(string[] args) =>
		Create<App>(args, (_, _) => { });

	/// <inheritdoc cref="Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (IHost app, ILog<App> log) Create(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		Create<App>(args, configureServices);

	/// <inheritdoc cref="Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (IHost app, ILog<T> log) Create<T>(string[] args)
		where T : App, new() =>
		Create<T>(args, (_, _) => { });

	/// <summary>
	/// Build an <see cref="IHost"/> with default Jeebs configuration.
	/// </summary>
	/// <inheritdoc cref="CreateBuilder{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	internal static (IHost app, ILog<T> log) Create<T>(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices)
		where T : App, new()
	{
		// Build host
		var host = CreateBuilder<T>(args, configureServices).Build();

		// Get log service
		var log = host.Services.GetRequiredService<ILog<T>>();

		// App is ready
		var app = new T();
		app.Ready(host.Services, log);

		// Return host and log
		return (host, log);
	}

	#endregion Create

	#region Create Builder

	/// <inheritdoc cref="CreateBuilder{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static IHostBuilder CreateBuilder(string[] args) =>
		CreateBuilder<App>(args, (_, _) => { });

	/// <inheritdoc cref="CreateBuilder{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static IHostBuilder CreateBuilder(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		CreateBuilder<App>(args, configureServices);

	/// <inheritdoc cref="CreateBuilder{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static IHostBuilder CreateBuilder<T>(string[] args)
		where T : App, new() =>
		CreateBuilder<T>(args, (_, _) => { });

	/// <summary>
	/// Create an <see cref="IHostBuilder"/> with default Jeebs configuration.
	/// </summary>
	/// <remarks>
	///   - Default configuration is loaded using <see cref="Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(string[])"/><br/>
	///   - Jeebs configuration is loaded from JSON files and (optionally) Azure Key Vault<br/>
	///   - Options classes are bound to configuration<br/>
	///   - Default services are registered with the DI container<br/>
	///   - Logging is enabled using Serilog
	/// </remarks>
	/// <typeparam name="T">App type</typeparam>
	/// <param name="args">Command-line arguments</param>
	/// <param name="configureServices">Add additional services</param>
	internal static IHostBuilder CreateBuilder<T>(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices)
		where T : App, new()
	{
		// Create app
		var app = new T();

		// Create and configure host
		return Microsoft.Extensions.Hosting.Host
			.CreateDefaultBuilder(args)
			.ConfigureHostConfiguration(app.ConfigureHost)
			.ConfigureAppConfiguration(app.ConfigureApp)
			.ConfigureServices(app.ConfigureServices)
			.ConfigureServices(configureServices)
			.UseSerilog(app.ConfigureSerilog);
	}

	#endregion Create Builder
}
