using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AppConsole
{
	public sealed class Program : Jeebs.Apps.Program
	{
		private static async Task Main(string[] args) => await Main<App>(args, (provider, config) =>
		{
			using var log = provider.GetService<ILog<Program>>();
			if (log == null)
			{
				return;
			}

			Serilog.Debugging.SelfLog.Enable(Console.Error);
			var jeebs = config.GetJeebsConfig();

			log.Debug("Services loaded");
			log.Debug("Project {Name}", jeebs.App.Name);

			log.Debug("Version: {0}", Version<DateRange>.Full);

			log.Error("Test error");
			log.Error(new Exception("Test"), "Something went badly wrong {here}", "just now");

			log.Critical(new Exception("Fatal"), "Something went fatally wrong {here}", "just now");

			var seq = provider.GetService<Seq>();
			seq?.Send("test");

			var slack = provider.GetService<Slack>();
			slack?.Send("test");

			var notifier = provider.GetService<INotifier>();
			notifier?.Send("test notification");

			while (Console.ReadLine() is string output)
			{
				log.Information(output);
			}
		});
	}
}
