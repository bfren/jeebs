using System;
using System.Collections.Generic;
using System.Text;
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
		public static async Task Main<T>(string[] args, Action<IServiceProvider, IConfiguration>? run = null)
			where T : App, new()
		{
			// Create app
			var app = new T();

			// Create host
			using var host = app.CreateHost(args);

			// Run default
			if (run is null)
			{
				await host.RunAsync().ConfigureAwait(false);
			}

			// Run custom
			else
			{
				var config = host.Services.GetService<IConfiguration>();
				run(host.Services, config);
			}
		}
	}
}
