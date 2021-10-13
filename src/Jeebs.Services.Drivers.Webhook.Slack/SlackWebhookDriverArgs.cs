// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Net.Http;
using Jeebs.Config;
using Microsoft.Extensions.Options;

namespace Jeebs.Services.Drivers.Webhook.Slack;

/// <summary>
/// Slack Webhook Driver arguments
/// </summary>
public sealed class SlackWebhookDriverArgs : WebhookDriverArgs<SlackConfig>
{
	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="factory">IHttpClientFactory</param>
	/// <param name="log">ILog</param>
	/// <param name="jeebsConfig">JeebsConfig</param>
	public SlackWebhookDriverArgs(
		IHttpClientFactory factory,
		ILog log,
		IOptions<JeebsConfig> jeebsConfig
	) : base(factory, log, jeebsConfig, c => c.Slack) { }
}
