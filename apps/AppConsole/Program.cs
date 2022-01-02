// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppConsole;
using AppConsole.Messages;
using Jeebs;
using Jeebs.Config;
using Jeebs.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

await Jeebs.Apps.Program.MainAsync<App>(args, async (provider, log) =>
{
	Serilog.Debugging.SelfLog.Enable(Console.Error);
	var jeebs = provider.GetRequiredService<IOptions<JeebsConfig>>().Value;

	log.Debug("Services loaded");
	log.Debug("Project {Name}", jeebs.App.Name);

	log.Debug("Version: {0}", await F.VersionF.GetJeebsVersionAsync().ConfigureAwait(false));

	log.Error("Test error");
	log.Error(new Exception("Test"), "Something went badly wrong {here}", "just now");

	log.Fatal(new Exception("Fatal"), "Something went fatally wrong {here}", "just now");

	var seq = provider.GetRequiredService<Seq>();
	seq.Send("test");

	var slack = provider.GetRequiredService<Slack>();
	slack.Send("test");

	var notifier = provider.GetRequiredService<INotifier>();
	notifier.Send("test notification");

	var one = async Task<Option<int>> (int input) =>
		await Task.FromResult(input + 1).ConfigureAwait(false);

	var two = async Task<Option<string>> (int input) =>
		await Task.FromResult(input.ToString()).ConfigureAwait(false);

	var three = async Task<Option<bool>> (string input) =>
		await Task.FromResult(input == "3").ConfigureAwait(false);

	var result = from r0 in one(2)
				 from r1 in two(r0)
				 from r2 in three(r1)
				 select r2;

	(await result.ConfigureAwait(false)).Audit(
		some: x => log.Information("Result: {0}", x),
		none: _ => log.Information("No result")
	);

	// Test IMsg output
	log.Information("Testing Msg");
	log.Message(new Basic());

	log.Information("Testing Msg with generic argument");
	log.Message(new WithGeneric<Guid>());

	log.Information("Testing Msg with format");
	log.Message(new FormattedMsg());

	log.Information("Testing Msg with value");
	log.Message(new WithValue(F.Rnd.Str));

	log.Information("Testing Msg with exception");
	var e = new Exception(F.Rnd.Str);
	log.Message(new WithException(e));

	// Log console
	while (Console.ReadLine() is string output)
	{
		log.Information(output);
	}
}).ConfigureAwait(false);
