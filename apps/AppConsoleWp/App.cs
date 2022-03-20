// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppConsoleWp.Bcg;
using AppConsoleWp.Usa;
using Jeebs.WordPress;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppConsoleWp;

/// <summary>
/// WordPress Console Application
/// </summary>
internal sealed class App : Jeebs.Apps.App
{
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		// Base
		base.ConfigureServices(ctx, services);

		// Add WordPress
		_ = services.AddWordPressInstance("bcg").Using<WpBcg, WpBcgConfig>(ctx.Configuration);
		_ = services.AddWordPressInstance("usa").Using<WpUsa, WpUsaConfig>(ctx.Configuration);
	}
}
