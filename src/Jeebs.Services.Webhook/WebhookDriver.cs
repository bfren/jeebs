// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Jeebs.Services.Webhook.Models;
using Microsoft.Extensions.DependencyInjection;
using static F.ThreadF;

namespace Jeebs.Services.Webhook
{
	/// <inheritdoc cref="IWebhookDriver{TConfig, TMessage}"/>
	public abstract class WebhookDriver<TConfig, TMessage> : Driver<TConfig>, IWebhookDriver<TConfig, TMessage>
		where TConfig : IWebhookServiceConfig
		where TMessage : notnull
	{
		/// <summary>
		/// Add required services - called by <see cref="ServiceCollectionExtensions"/>
		/// </summary>
		/// <param name="services">IServiceCollection</param>
#pragma warning disable RCS1158 // Static member in generic type should use a type parameter.
		public static void AddRequiredServices(IServiceCollection services) =>
#pragma warning restore RCS1158 // Static member in generic type should use a type parameter.

			services.AddHttpClient();

		/// <summary>
		/// IHttpClientFactory
		/// </summary>
		private readonly IHttpClientFactory factory;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">WebhookServiceArgs</param>
		protected WebhookDriver(string name, WebhookDriverArgs<TConfig> args) : base(name, args) =>
			factory = args.Factory;

		#region Convert to Jeebs.Services.Webhook.Models.Message and Send

		/// <inheritdoc/>
		public void Send(string message, NotificationLevel level = NotificationLevel.Information) =>
			Send(new Message { Content = message, Level = level });

		/// <inheritdoc/>
		public virtual void Send(IMsg msg)
		{
			// Get message content
			var content = msg.ToString() ?? msg.GetType().ToString();

			// Convert to notification Message
			var message = msg switch
			{
				ILogMsg x =>
					new Message
					{
						Content = content,
						Level = x.Level.ToNotificationLevel()
					},

				IExceptionMsg x =>
					new Message
					{
						Content = content,
						Level = NotificationLevel.Error,
						Fields = new Dictionary<string, object>()
						{
							{ "Exception", x.Exception }
						}
					},

				_ =>
					new Message
					{
						Content = content,
						Level = NotificationLevel.Information
					}
			};

			// Send message
			Send(message);
		}

		#endregion

		#region Convert to System.Net.Http.HttpRequestMessage and Send

		/// <inheritdoc/>
		public abstract void Send(IWebhookMessage message);

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

		#endregion

		#region Actually Send

		/// <summary>
		/// Use <see cref="factory"/> to send the message
		/// </summary>
		/// <param name="request"></param>
		protected void Send(HttpRequestMessage request) =>
			FireAndForget(async () =>
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

		#endregion
	}
}
