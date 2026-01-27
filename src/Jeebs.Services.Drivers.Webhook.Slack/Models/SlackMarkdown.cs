// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack block content - markdown.
/// </summary>
/// <param name="Text">Markdown text.</param>
public sealed record class SlackMarkdown(string Text) : SlackText("mrkdwn", Text);
