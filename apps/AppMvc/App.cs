// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing;
using Jeebs.Services.Drivers.Drawing.Skia;
using Microsoft.AspNetCore.Mvc;
using Wrap.Extensions;

namespace AppMvc;

public sealed class App : Jeebs.Apps.Web.MvcApp
{
	public App() : base(false) { }

	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);
		services.AddTransient<IImageDriver, ImageDriver>();
	}

	protected override void ConfigureServicesMvcOptions(HostBuilderContext ctx, MvcOptions opt)
	{
		base.ConfigureServicesMvcOptions(ctx, opt);
		opt.AddIdModelBinder();
	}
}
