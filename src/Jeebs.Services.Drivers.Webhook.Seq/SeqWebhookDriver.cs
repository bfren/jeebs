// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.Net.Http;
using Jeebs.Config;
using Jeebs.Services.Webhook;

namespace Jeebs.Services.Drivers.Webhook.Seq
{
	/// <summary>
	/// Seq Webhook driver
	/// </summary>
	public abstract class SeqWebhookDriver : WebhookDriver<SeqConfig, SeqEvent>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">SeqWebhookDriverArgs</param>
		protected SeqWebhookDriver(string name, SeqWebhookDriverArgs args) : base(name, args) { }

		/// <inheritdoc/>
		public override void Send(IWebhookMessage message) =>
			Send(new SeqEvent(message.Content, message.Level));

		/// <inheritdoc/>
		public override void Send(SeqEvent message)
		{
			// Build request message
			var request = new HttpRequestMessage(HttpMethod.Post, ServiceConfig.Webhook);
			request.Headers.Add("X-Seq-ApiKey", ServiceConfig.ApiKey);

			// Add content
			request.Content = new JsonHttpContent(message, "application/vnd.serilog.clef");

			// Send request
			Send(request);
		}
	}
}
