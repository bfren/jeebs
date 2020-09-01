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
	public abstract class SeqWebhookDriver : WebhookDriver<SeqConfig>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">SeqWebhookDriverArgs</param>
		protected SeqWebhookDriver(string name, SeqWebhookDriverArgs args) : base(name, args) { }

		/// <inheritdoc/>
		public override void Send(Message message)
		{
			// Build request message
			var uri = $"{Config.Server}/api/events/raw?clef";
			var request = new HttpRequestMessage(HttpMethod.Post, uri);
			request.Headers.Add("X-Seq-ApiKey", Config.ApiKey);

			// Create event and add to the message
			var e = new SeqEvent(message.Content, message.Level);
			request.Content = new SeqHttpContent(e);

			// Send request
			Send(request);
		}
	}
}
