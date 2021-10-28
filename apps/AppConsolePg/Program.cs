// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppConsolePg;

await Jeebs.Apps.Program.MainAsync<App>(args, async (provider, log) =>
{
	// Begin
	log.Debug("= PostgreSQL Console Test =");

});
