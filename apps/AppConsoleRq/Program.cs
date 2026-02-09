// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Clients.Rqlite;
using Microsoft.Extensions.DependencyInjection;
using Rqlite.Client;

var (app, log) = Jeebs.Apps.Host.Create(args, (ctx, services) =>
{
	services.AddRqlite();
	services.AddData<AppConsoleRq.Db, RqliteDbClient>();
});

var db = app.Services.GetRequiredService<IDb>();
var dbTyped = app.Services.GetRequiredService<AppConsoleRq.Db>();

using var w = dbTyped.StartWork();
var version = await w.GetVersionAsync();
log.Inf("Version: {Version}", version);
