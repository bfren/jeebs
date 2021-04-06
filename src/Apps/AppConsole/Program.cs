// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using AppConsole;
using Jeebs;
using Jeebs.Config;
using Jeebs.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

await Jeebs.Apps.Program.MainAsync<App>(args,
	async (provider, config) =>
	{
		var log = provider.GetRequiredService<ILog<App>>();

		Serilog.Debugging.SelfLog.Enable(Console.Error);
		var jeebs = provider.GetRequiredService<IOptions<JeebsConfig>>().Value;

		log.Debug("Services loaded");
		log.Debug("Project {Name}", jeebs.App.Name);

		log.Debug("Version: {0}", await F.VersionF.GetJeebsVersionAsync());

		log.Error("Test error");
		log.Error(new Exception("Test"), "Something went badly wrong {here}", "just now");

		log.Fatal(new Exception("Fatal"), "Something went fatally wrong {here}", "just now");

		var seq = provider.GetRequiredService<Seq>();
		seq.Send("test");

		var slack = provider.GetRequiredService<Slack>();
		slack.Send("test");

		var notifier = provider.GetRequiredService<INotifier>();
		notifier.Send("test notification");

		async Task<Option<int>> one(int input) =>
			await Task.FromResult(input + 1);

		async Task<Option<string>> two(int input) =>
			await Task.FromResult(input.ToString());

		async Task<Option<bool>> three(string input) =>
			await Task.FromResult(input == "3");

		var result = from r0 in one(2)
					 from r1 in two(r0)
					 from r2 in three(r1)
					 select r2;

		(await result).Audit(
			some: x => log.Information("Result: {0}", x),
			none: _ => log.Information("No result")
		);

		while (Console.ReadLine() is string output)
		{
			log.Information(output);
		}
	}
);
