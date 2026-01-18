// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack message block divider.
/// </summary>
public sealed record class SlackDivider() : SlackBlock("divider");
