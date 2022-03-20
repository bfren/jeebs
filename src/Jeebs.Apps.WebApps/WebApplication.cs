// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MS = Microsoft.AspNetCore.Builder;

namespace Jeebs.Apps.WebApps;

/// <summary>
/// Uses .NET 6 web hosting model, with default options
/// </summary>
public static class WebApplication
{
	/// <inheritdoc cref="Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static MS.WebApplication Create(string[] args) =>
		Create<WebApp>(args, (_, _) => { });

	/// <inheritdoc cref="Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static MS.WebApplication Create(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		Create<WebApp>(args, configureServices);

	/// <inheritdoc cref="Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static MS.WebApplication Create<T>(string[] args)
		where T : WebApp, new() =>
		Create<T>(args, (_, _) => { });

	/// <summary>
	/// Create <see cref="MS.WebApplication"/> with default Jeebs configuration
	/// </summary>
	/// <remarks>
	///   - Default configuration is loaded using <see cref="Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(string[])"/><br/>
	///   - Jeebs configuration is loaded from JSON files and (optionally) Azure Key Vault<br/>
	///   - Options classes are bound to configuration<br/>
	///   - Default services are registered with the DI container<br/>
	///   - Logging is enabled using Serilog<br/>
	///   - <see cref="MS.WebApplication"/> is built<br/>
	///   - <see cref="MS.WebApplication"/> is configured
	/// </remarks>
	/// <typeparam name="T">App type</typeparam>
	/// <param name="args">Command-line arguments</param>
	/// <param name="configureServices">Add additional services</param>
	internal static MS.WebApplication Create<T>(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices)
		where T : WebApp, new()
	{
		// Create app and host
		var app = new T();
		var builder = MS.WebApplication.CreateBuilder(args);

		// Configure host
		_ = builder
			.Host.ConfigureHostConfiguration(app.ConfigureHost)
			.ConfigureAppConfiguration(app.ConfigureApp)
			.ConfigureServices(app.ConfigureServices)
			.ConfigureServices(configureServices)
			.UseSerilog(app.ConfigureSerilog);

		// Build host
		var webApplication = builder.Build();

		// Configure application
		app.Configure(webApplication);
		return webApplication;
	}
}
