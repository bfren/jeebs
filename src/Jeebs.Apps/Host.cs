// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jeebs.Apps;

/// <summary>
/// Uses .NET 6 hosting model, with default options
/// </summary>
public static class Host
{
	/// <inheritdoc cref="CreateBuilder{T}(string[]"/>
	public static IHostBuilder CreateBuilder(string[] args) =>
		CreateBuilder<App>(args);

	/// <summary>
	/// Create <see cref="IHostBuilder"/> with default Jeebs configuration
	/// </summary>
	/// <remarks>
	///   - Default configuration is loaded using <see cref="Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder"/><br/>
	///   - Jeebs configuration is loaded from JSON files and (optionally) Azure Key Vault<br/>
	///   - Options classes are bound to configuration<br/>
	///   - Default services are registered with the DI container<br/>
	///   - Logging is enabled using Serilog
	/// </remarks>
	/// <typeparam name="T">App type</typeparam>
	/// <param name="args">Command-line arguments</param>
	public static IHostBuilder CreateBuilder<T>(string[] args)
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
			.UseSerilog(app.ConfigureSerilog);
	}
}
