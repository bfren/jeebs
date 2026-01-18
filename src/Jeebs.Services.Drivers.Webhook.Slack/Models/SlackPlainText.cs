// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack block content - plain text.
/// </summary>
/// <param name="Text">Plain text.</param>
public sealed record class SlackPlainText(string Text) : SlackText("plain_text", Text);
