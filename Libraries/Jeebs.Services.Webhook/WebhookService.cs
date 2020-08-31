using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs.Config;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Webhook
{
	/// <inheritdoc cref="IWebhookService{TConfig}"/>
	public abstract class WebhookService<TConfig> : Service<TConfig>, IWebhookService<TConfig>
		where TConfig : ServiceConfig
	{
		private readonly IHttpClientFactory factory;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="log">ILog</param>
		/// <param name="config">JeebsConfig</param>
		/// <param name="getCollection">Function to return service configuration collection for this service type</param>
		/// <param name="factory">IHttpClientFactory</param>
		protected WebhookService(string name, ILog log, JeebsConfig config, Func<ServicesConfig, Dictionary<string, TConfig>> getCollection, IHttpClientFactory factory)
			: base(name, log, config, getCollection)
			=> this.factory = factory;

		/// <inheritdoc/>
		public void Send(string message, MessageLevel level = MessageLevel.Information)
			=> Send(new Message { Content = message, Level = level });

		/// <inheritdoc/>
		public abstract void Send(Message message);

		/// <summary>
		/// Use <see cref="factory"/> to send the message
		/// </summary>
		/// <param name="request"></param>
		protected void Send(HttpRequestMessage request)
		{
			var client = factory.CreateClient();

			try
			{
				Log.Trace("Sending message: {@Request}", request);

				var response = client.SendAsync(request).GetAwaiter().GetResult();
				if (!response.IsSuccessStatusCode)
				{
					Log.Warning("Unable to send message: {@Response}", response);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error sending message: {@Request}", request);
			}
		}
	}
}
