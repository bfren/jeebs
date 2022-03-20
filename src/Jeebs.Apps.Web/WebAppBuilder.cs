// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jeebs.Apps.Web;

/// <summary>
/// Uses .NET 6 web hosting model, with default options
/// </summary>
internal static class WebAppBuilder
{
	/// <summary>
	/// Create <see cref="WebApplication"/> with default Jeebs configuration
	/// </summary>
	/// <remarks>
	///   - Default configuration is loaded using <see cref="Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(string[])"/><br/>
	///   - Jeebs configuration is loaded from JSON files and (optionally) Azure Key Vault<br/>
	///   - Options classes are bound to configuration<br/>
	///   - Default services are registered with the DI container<br/>
	///   - Logging is enabled using Serilog<br/>
	///   - <see cref="WebApplication"/> is built<br/>
	///   - <see cref="WebApplication"/> is configured
	/// </remarks>
	/// <typeparam name="T">App type</typeparam>
	/// <param name="args">Command-line arguments</param>
	/// <param name="configureServices">Add additional services</param>
	internal static WebApplication Create<T>(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices)
		where T : WebApp, new()
	{
		// Create app and host
		var app = new T();
		var builder = WebApplication.CreateBuilder(args);

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
