using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs.Config;
using Jeebs.Services.Webhook;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Drivers.Webhook.Seq
{
	/// <summary>
	/// Seq service
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
		public override void Send(Message message)
			=> Send(new SeqEvent(message.Content, message.Level));

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
