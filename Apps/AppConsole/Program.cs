// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AppConsole
{
	public sealed class Program : Jeebs.Apps.Program
	{
		private static async Task Main(string[] args) =>
			await Main<App>(
				args,
				(provider, config) =>
				{
					var log = provider.GetRequiredService<ILog<Program>>();

					Serilog.Debugging.SelfLog.Enable(Console.Error);
					var jeebs = config.GetJeebsConfig();

					log.Debug("Services loaded");
					log.Debug("Project {Name}", jeebs.App.Name);

					log.Debug("Version: {0}", Version<DateRange>.Full);

					log.Error("Test error");
					log.Error(new Exception("Test"), "Something went badly wrong {here}", "just now");

					log.Critical(new Exception("Fatal"), "Something went fatally wrong {here}", "just now");

					var seq = provider.GetRequiredService<Seq>();
					seq.Send("test");

					var slack = provider.GetRequiredService<Slack>();
					slack.Send("test");

					var notifier = provider.GetRequiredService<INotifier>();
					notifier.Send("test notification");

					while (Console.ReadLine() is string output)
					{
						log.Information(output);
					}
				}
			);
	}
}
