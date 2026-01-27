// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drivers.Webhook.Slack;
using Jeebs.Services.Notify;

namespace AppConsole;

internal sealed class Slack(SlackWebhookDriverArgs args) : SlackWebhookDriver("bcgdesign", args), INotificationListener;
