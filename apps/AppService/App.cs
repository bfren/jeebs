// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Microsoft.Extensions.Hosting;

namespace ServiceApp;

public class App : Jeebs.Apps.ServiceApp<AppService>;

public class AppService(ILog log) : IHostedService
{
	public Task StartAsync(CancellationToken cancellationToken)
	{
		log.Dbg("Hello, world!");

		if (Console.ReadLine() is string response)
		{
			log.Dbg("Response: {Response}", response);
		}

		Console.Read();

		return Task.Delay(2000, cancellationToken);
	}

	public Task StopAsync(CancellationToken cancellationToken) =>
		Task.CompletedTask;
}
