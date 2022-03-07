﻿// Jeebs Test Applications
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

	log.Dbg("Services loaded");
	log.Dbg("Project {Name}", jeebs.App.Name);

	log.Dbg("Version: {0}", await F.VersionF.GetJeebsVersionAsync().ConfigureAwait(false));

	log.Err("Test error");
	log.Err(new Exception("Test"), "Something went badly wrong {here}", "just now");

	log.Die(new Exception("Fatal"), "Something went fatally wrong {here}", "just now");

	var seq = provider.GetRequiredService<Seq>();
	seq.Send("test");

	var slack = provider.GetRequiredService<Slack>();
	slack.Send("test");

	var notifier = provider.GetRequiredService<INotifier>();
	notifier.Send("test notification");

	var one = async Task<Maybe<int>> (int input) =>
		await Task.FromResult(input + 1).ConfigureAwait(false);

	var two = async Task<Maybe<string>> (int input) =>
		await Task.FromResult(input.ToString()).ConfigureAwait(false);

	var three = async Task<Maybe<bool>> (string input) =>
		await Task.FromResult(input == "3").ConfigureAwait(false);

	var result = from r0 in one(2)
				 from r1 in two(r0)
				 from r2 in three(r1)
				 select r2;

	(await result.ConfigureAwait(false)).Audit(
		some: x => log.Inf("Result: {0}", x),
		none: _ => log.Inf("No result")
	);

	// Test IMsg output
	log.Inf("Testing Msg");
	log.Msg(new Basic());

	log.Inf("Testing Msg with generic argument");
	log.Msg(new WithGeneric<Guid>());

	log.Inf("Testing Msg with format");
	log.Msg(new FormattedMsg());

	log.Inf("Testing Msg with value");
	log.Msg(new WithValue(F.Rnd.Str));

	log.Inf("Testing Msg with exception");
	var e = new Exception(F.Rnd.Str);
	log.Msg(new WithException(e));

	// Log console
	while (Console.ReadLine() is string output)
	{
		log.Inf(output);
	}
}).ConfigureAwait(false);
