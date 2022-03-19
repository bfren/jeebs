// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Hosting;
using ServiceApp;

Jeebs.Apps.Host.CreateBuilder<App>(args).Build().Run();
