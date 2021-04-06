// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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
		/// <param name="run">[Optional] Action to run program with IServiceProvider and IConfiguration</param>
		public static async Task MainAsync<T>(string[] args, Action<IServiceProvider, IConfiguration>? run = null)
			where T : App, new()
		{
			// Create app
			var app = new T();

			// Create host
			using var host = app.CreateHost(args);

			// Ready to go
			app.Ready(host.Services, host.Services.GetRequiredService<ILog<T>>());

			// Run default
			if (run is null)
			{
				await host.RunAsync().ConfigureAwait(false);
			}

			// Run custom
			else
			{
				var config = host.Services.GetRequiredService<IConfiguration>();
				run(host.Services, config);
			}
		}
	}
}
