// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading;
using System.Threading.Tasks;
using Jeebs;
using Microsoft.Extensions.Hosting;

namespace ServiceApp
{
	public class App : Jeebs.Apps.ServiceApp<AppService>
	{
	}

	public class AppService : IHostedService
	{
		private readonly ILog log;

		public AppService(ILog log)
		{
			this.log = log;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			log.Debug("Hello, world!");

			if (Console.ReadLine() is string response)
			{
				log.Debug("Response: {Response}", response);
			}

			Console.Read();

			return Task.Delay(2000, cancellationToken);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
