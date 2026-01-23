// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Net.Http;
using Jeebs.Config;
using Jeebs.Functions;
using Jeebs.Services.Notify;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Webhook;

/// <inheritdoc cref="IWebhookDriver{TConfig, TMessage}"/>
public abstract class WebhookDriver<TConfig, TMessage> : Driver<TConfig>, IWebhookDriver<TConfig, TMessage>
	where TConfig : IWebhookServiceConfig, new()
	where TMessage : notnull
{
	/// <summary>
	/// IHttpClientFactory.
	/// </summary>
	internal IHttpClientFactory Factory { get; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="name">Service name.</param>
	/// <param name="args">WebhookServiceArgs.</param>
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
	public virtual void Send(FailValue failure)
	{
		// Get fields
		var fields = DictionaryF.FromObject(failure.Args ?? new());

		// Convert to notification Message
		var message = new Message
		{
			Content = failure.Message.FormatWith(failure.Args),
			Level = failure.Level.ToNotificationLevel(),
			Fields = failure.Exception switch
			{
				{ } e =>
					fields.WithItem("Exception", e).ToDictionary(),

				_ =>
					fields.ToDictionary()
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
	/// Use <see cref="Factory"/> to send the message.
	/// </summary>
	/// <param name="request"></param>
	protected void Send(HttpRequestMessage request) =>
		ThreadF.FireAndForget(async () =>
		{
			try
			{
				var client = Factory.CreateClient();
				var response = await client.SendAsync(request);
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

public static class PrototypeExtensions
{
	public static IDictionary<string, object> ToDictionary<T>(this object anon)
	{
		var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		if (anon == null)
		{
			return dict;
		}

		foreach (var info in anon.GetType().GetProperties())
		{
			if (info.GetValue(anon) is object value)
			{
				dict.Add(info.Name, value);
			}
		}

		return dict;
	}
}
