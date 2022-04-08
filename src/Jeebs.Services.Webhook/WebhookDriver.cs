// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Net.Http;
using Jeebs.Config;
using Jeebs.Functions;
using Jeebs.Messages;
using Jeebs.Services.Notify;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Webhook;

/// <inheritdoc cref="IWebhookDriver{TConfig, TMessage}"/>
public abstract class WebhookDriver<TConfig, TMessage> : Driver<TConfig>, IWebhookDriver<TConfig, TMessage>
	where TConfig : IWebhookServiceConfig, new()
	where TMessage : notnull
{
	/// <summary>
	/// IHttpClientFactory
	/// </summary>
	internal IHttpClientFactory Factory { get; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="name">Service name</param>
	/// <param name="args">WebhookServiceArgs</param>
	protected WebhookDriver(string name, WebhookDriverArgs<TConfig> args) : base(name, args) =>
		Factory = args.Factory;

	#region Convert to Jeebs.Services.Webhook.Models.Message and Send

	/// <inheritdoc/>
	public void Send(string message) =>
		Send(new Message { Content = message, Level = NotificationLevel.Information });

	/// <inheritdoc/>
	public void Send(string message, NotificationLevel level) =>
		Send(new Message { Content = message, Level = level });

	/// <inheritdoc/>
	public virtual void Send(IMsg msg)
	{
		// Get message content
		var content = msg.ToString() ?? msg.GetType().ToString();

		// Convert to notification Message
		var message = msg switch
		{
			ExceptionMsg x =>
				new Message
				{
					Content = content,
					Level = NotificationLevel.Error,
					Fields = new Dictionary<string, object>
					{
						{ "Exception", x.Value }
					}
				},

			Msg x =>
				new Message
				{
					Content = content,
					Level = x.Level.ToNotificationLevel()
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

	#endregion Convert to Jeebs.Services.Webhook.Models.Message and Send

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

	#endregion Convert to System.Net.Http.HttpRequestMessage and Send

	#region Actually Send

	/// <summary>
	/// Use <see cref="Factory"/> to send the message
	/// </summary>
	/// <param name="request"></param>
	protected void Send(HttpRequestMessage request) =>
		ThreadF.FireAndForget(async () =>
		{
			try
			{
				var client = Factory.CreateClient();
				var response = await client.SendAsync(request).ConfigureAwait(false);
				if (!response.IsSuccessStatusCode)
				{
					Log.Wrn("Unable to send message: {@Response}", response);
				}
			}
			catch (Exception ex)
			{
				Log.Err(ex, "Error sending message: {@Request}", request);
			}
		});

	#endregion Actually Send
}
