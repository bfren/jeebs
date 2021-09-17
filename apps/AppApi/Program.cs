// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Builder;

var (app, log) = Jeebs.Apps.Builder.Create(args);

app.MapGet("/", () => "Hello, world!");

app.Run();
