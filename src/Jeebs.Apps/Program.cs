// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps;

/// <summary>
/// Run a Program using <see cref="App"/>
/// </summary>
public abstract class Program
{
	/// <summary>
	/// Synchronous entry point
	/// </summary>
	/// <typeparam name="T">Host type</typeparam>
	/// <param name="args">Command Line arguments</param>
	public static void Main<T>(string[] args)
		where T : App, new() =>
		Main<T>(args, null);

	/// <summary>
	/// Synchronous entry point
	/// </summary>
	/// <typeparam name="T">Host type</typeparam>
	/// <param name="args">Command Line arguments</param>
	/// <param name="run">Action to run with IServiceProvider and ILog</param>
	public static void Main<T>(string[] args, Action<IServiceProvider, ILog>? run)
		where T : App, new() =>
		MainAsync<T>(args, (provider, log) => { run?.Invoke(provider, log); return Task.CompletedTask; }).RunSynchronously();

	/// <summary>
	/// Asynchronous entry point
	/// </summary>
	/// <typeparam name="T">Host type</typeparam>
	/// <param name="args">Command Line arguments</param>
	public static Task MainAsync<T>(string[] args)
		where T : App, new() =>
		MainAsync<T>(args, null);

	/// <summary>
	/// Asynchronous entry point
	/// </summary>
	/// <typeparam name="T">Host type</typeparam>
	/// <param name="args">Command Line arguments</param>
	/// <param name="run">Asynchronous function to run with IServiceProvider and ILog</param>
	public static async Task MainAsync<T>(string[] args, Func<IServiceProvider, ILog, Task>? run)
		where T : App, new()
	{
		// Create app
		var app = new T();

		// Build host
		using var host = app.BuildHost(args);

		// Get log for this app
		var log = host.Services.GetRequiredService<ILog<T>>();

		// Ready to go
		app.Ready(host.Services, log);

		// Run custom app
		if (run is not null)
		{
			await run(host.Services, log).ConfigureAwait(false);
		}

		// Run default app
		else
		{
			await host.RunAsync().ConfigureAwait(false);
		}
	}
}
