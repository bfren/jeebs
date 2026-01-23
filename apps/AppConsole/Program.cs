// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppConsole;
using Jeebs.Config;
using Jeebs.Functions;
using Jeebs.Services;
using Jeebs.Services.Notify;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RndF;
using Wrap;
using Wrap.Linq;

var (app, log) = Jeebs.Apps.Host.Create(args, (_, services) => services.AddDrivers());

Serilog.Debugging.SelfLog.Enable(Console.Error);
var jeebs = app.Services.GetRequiredService<IOptions<JeebsConfig>>().Value;

log.Dbg("Services loaded");
log.Dbg("Project {Name}", jeebs.App.Name);

log.Dbg("Version: {0}", await VersionF.GetJeebsVersionAsync());

log.Err("Test error");
log.Err(new Exception("Test"), "Something went badly wrong {here}", "just now");

log.Ftl(new Exception("Fatal"), "Something went fatally wrong {here}", "just now");

var slack = app.Services.GetRequiredService<Slack>();
slack.Send("test ok");
slack.Send("test warning", NotificationLevel.Warning);
slack.Send("test error", NotificationLevel.Error);

var notifier = app.Services.GetRequiredService<INotifier>();
notifier.Send("test notification");

var one = async Task<Result<int>> (int input) =>
	await Task.FromResult(input + 1);

var two = async Task<Result<string>> (int input) =>
	await Task.FromResult(input.ToString());

var three = async Task<Result<bool>> (string input) =>
	await Task.FromResult(input == "3");

var result = from r0 in one(2)
			 from r1 in two(r0)
			 from r2 in three(r1)
			 select r2;

(await result).Audit(
	ok: x => log.Inf("Result: {0}", x),
	fail: _ => log.Inf("No result")
);

// Test exception output
log.Inf("Testing exception output");
var ex = new InvalidOperationException(Rnd.Str, new NullReferenceException(nameof(Program)));
log.Err(ex, "Testing Failure with exception.");
log.Err("Testing Failure with exception object: {@Exception}.", ex);

// Test log of Result failure
log.Inf("Testing Result failure");
R.Try(
	() => Basic.DoSomething(Rnd.Str),
	e => R.Fail(e).Ctx(nameof(Program), "Main")
).Audit(fail: log.Failure);

// Test Microsoft ILogger
var microsoftLogTest = LoggerMessage.Define<string>(LogLevel.Information, new(), "{Msg}");
var msLog = app.Services.GetRequiredService<ILogger<Basic>>();
var msMsg = "Test from Microsoft Logging framework";
microsoftLogTest(msLog, msMsg, null);

// Log console
while (Console.ReadLine() is string output)
{
	log.Inf(output);
}
