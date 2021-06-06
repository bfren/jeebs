// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps
{
	/// <summary>
	/// Run a Program using <see cref="App"/>
	/// </summary>
	public abstract class Program
	{
		/// <summary>
		/// Entry point
		/// </summary>
		/// <typeparam name="T">Host type</typeparam>
		/// <param name="args">Command Line arguments</param>
		/// <param name="run">[Optional] Action to run program with IServiceProvider and ILog</param>
		public static async Task MainAsync<T>(string[] args, Action<IServiceProvider, ILog>? run = null)
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

			// Run default app
			if (run is null)
			{
				await host.RunAsync().ConfigureAwait(false);
			}

			// Run custom app
			else
			{
				run(host.Services, log);
			}
		}
	}
}
