// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config;

/// <summary>
/// Seq configuration
/// </summary>
public sealed record class SeqConfig : IWebhookServiceConfig
{
	/// <inheritdoc/>
	public string Webhook =>
		$"{Server}/api/events/raw?clef";

	/// <summary>
	/// Seq Server URI
	/// </summary>
	public string Server { get; init; } = string.Empty;

	/// <summary>
	/// Seq Server API Key
	/// </summary>
	public string ApiKey { get; init; } = string.Empty;

	/// <inheritdoc/>
	public bool IsValid =>
		!string.IsNullOrEmpty(Server)
		&& !string.IsNullOrEmpty(ApiKey)
		&& F.UriF.IsHttps(Webhook);
}
