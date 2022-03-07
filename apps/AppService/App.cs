// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Microsoft.Extensions.Hosting;

namespace ServiceApp;

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
		log.Dbg("Hello, world!");

		if (Console.ReadLine() is string response)
		{
			log.Dbg("Response: {Response}", response);
		}

		Console.Read();

		return Task.Delay(2000, cancellationToken);
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
