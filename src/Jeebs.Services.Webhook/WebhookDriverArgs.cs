// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Net.Http;
using Jeebs.Config;
using Jeebs.Config.Services;
using Jeebs.Logging;
using Microsoft.Extensions.Options;

namespace Jeebs.Services;

/// <summary>
/// Webhook Driver arguments.
/// </summary>
/// <typeparam name="TConfig">Service configuration type.</typeparam>
public abstract class WebhookDriverArgs<TConfig> : DriverArgs<TConfig>
	where TConfig : IServiceConfig
{
	/// <summary>
	/// IHttpClientFactory.
	/// </summary>
	public IHttpClientFactory Factory { get; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="factory"></param>
	/// <param name="log">ILog.</param>
	/// <param name="jeebsConfig">JeebsConfig.</param>
	/// <param name="serviceConfigs">Function to return all service configurations for this type.</param>
	protected WebhookDriverArgs(
		IHttpClientFactory factory,
		ILog log,
		IOptions<JeebsConfig> jeebsConfig,
		Func<ServicesConfig, Dictionary<string, TConfig>> serviceConfigs
	) : base(log, jeebsConfig, serviceConfigs) =>
		Factory = factory;
}
