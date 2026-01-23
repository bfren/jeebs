// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Dynamic;
using Jeebs.Services.Notify;
using Jeebs.Services.Webhook;

namespace Jeebs.Services.Drivers.Webhook.Seq;

/// <summary>
/// Seq Event.
/// </summary>
public sealed record class SeqEvent
{
	/// <summary>
	/// Custom event properties.
	/// </summary>
	public ExpandoObject Properties { get; init; } = new()!;

	/// <summary>
	/// Create event.
	/// </summary>
	/// <param name="message"></param>
	/// <param name="level"></param>
	public SeqEvent(IWebhookMessage message)
	{
		var prop = Properties as IDictionary<string, object>;

		prop["@t"] = DateTime.Now.ToString("O");
		prop["@mt"] = message.Content;
		prop["@l"] = Enum.GetName(message.Level) ?? nameof(NotificationLevel.Information);

		foreach (var (key, value) in message.Fields)
		{
			prop[key] = value;
		}
	}
}
