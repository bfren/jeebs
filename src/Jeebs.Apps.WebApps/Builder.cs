// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Apps;

/// <summary>
/// Allows use of new Minimal API syntax
/// </summary>
public static class Builder
{
	/// <summary>
	/// Create a configured <see cref="WebApplication"/>
	/// </summary>
	/// <param name="args">Commandline arguments</param>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	/// <param name="configure">[Optional] Configure builder before app is built</param>
	public static (WebApplication, ILog) Create(string[] args, bool useHsts = true, Action<WebApplicationBuilder>? configure = null)
	{
		// Create app
		var app = new MinimalApiApp(useHsts).Create(args, configure);

		// Set Option Audit log
		var log = app.Services.GetRequiredService<ILog<MinimalApiApp>>();
		F.OptionF.LogAuditExceptions = e => log.Error(e, "Error auditing Option");

		// Ready to go
		log.Information("Application configured.");
		return (app, log);
	}
}
