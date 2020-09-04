using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using Jeebs.Config;
using Jeebs.Services.Webhook.Models;
using Microsoft.Extensions.DependencyInjection;
using static F.Thread;

namespace Jeebs.Services.Webhook
{
	/// <inheritdoc cref="IWebhookDriver{TConfig}"/>
	public abstract class WebhookDriver<TConfig, TMessage> : Driver<TConfig>, IWebhookDriver<TConfig, TMessage>
		where TConfig : WebhookServiceConfig
		where TMessage : notnull
	{
		/// <summary>
		/// Add required services - called by <see cref="ServiceCollectionExtensions"/>
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		public static void AddRequiredServices(IServiceCollection services)
			=> services.AddHttpClient();

		/// <summary>
		/// IHttpClientFactory
		/// </summary>
		private readonly IHttpClientFactory factory;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">WebhookServiceArgs</param>
		protected WebhookDriver(string name, WebhookDriverArgs<TConfig> args) : base(name, args)
			=> factory = args.Factory;

		/// <inheritdoc/>
		public void Send(string message)
			=> Send(message, MessageLevel.Information);

		/// <inheritdoc/>
		public void Send(string message, MessageLevel level)
			=> Send(new Message { Content = message, Level = level });

		/// <inheritdoc/>
		public virtual void Send(IMsg msg)
		{
			// Prepare IMsg
			var (text, args) = msg.Prepare();
			var content = text.FormatWith(args);

			// Convert to notification Message
			var message = msg switch
			{
				IExceptionMsg x
					=> new Message
					{
						Content = content,
						Level = MessageLevel.Error,
						Fields = new Dictionary<string, object>()
						{
							{ "Exception", x.Exception }
						}
					},
				ILoggableMsg x
					=> new Message
					{
						Content = content,
						Level = x.Level.ToMessageLevel()
					},
				{ } x
					=> new Message
					{
						Content = content,
						Level = MessageLevel.Information
					}
			};

			// Send message
			Send(message);
		}

		/// <inheritdoc/>
		public abstract void Send(Message message);

		/// <inheritdoc/>
		public virtual void Send(TMessage message)
		{
			// Build request message
			var request = new HttpRequestMessage(HttpMethod.Post, ServiceConfig.Webhook)
			{
				Content = new JsonHttpContent(message)
			};

			// Send request
			Send(request);
		}

		/// <summary>
		/// Use <see cref="factory"/> to send the message
		/// </summary>
		/// <param name="request"></param>
		protected void Send(HttpRequestMessage request)
			=> FireAndForget(async () =>
			{
				try
				{
					var client = factory.CreateClient();
					var response = await client.SendAsync(request).ConfigureAwait(false);
					if (!response.IsSuccessStatusCode)
					{
						Log.Warning("Unable to send message: {@Response}", response);
					}
				}
				catch (Exception ex)
				{
					Log.Error(ex, "Error sending message: {@Request}", request);
				}
			});
	}
}
