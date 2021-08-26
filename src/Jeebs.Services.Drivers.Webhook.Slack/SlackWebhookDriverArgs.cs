// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;
using Microsoft.Extensions.Options;

namespace Jeebs.Services.Drivers.Webhook.Slack
{
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
}
