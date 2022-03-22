// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Jeebs.Apps.Host.CreateBuilder(args);
var app = builder.Build();

var log = app.Services.GetRequiredService<ILog>();
log.Inf("Good morning.");

app.Run();
