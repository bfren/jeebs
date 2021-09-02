// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Twitter configuration
	/// </summary>
	public sealed record class TwitterConfig : IServiceConfig
	{
		/// <summary>
		/// UserAccessToken
		/// </summary>
		public string UserAccessToken { get; init; } = string.Empty;

		/// <summary>
		/// UserAccessSecret
		/// </summary>
		public string UserAccessSecret { get; init; } = string.Empty;

		/// <summary>
		/// ConsumerKey
		/// </summary>
		public string ConsumerKey { get; init; } = string.Empty;

		/// <summary>
		/// ConsumerSecret
		/// </summary>
		public string ConsumerSecret { get; init; } = string.Empty;

		/// <inheritdoc/>
		public bool IsValid =>
			!string.IsNullOrWhiteSpace(UserAccessToken)
			&& !string.IsNullOrWhiteSpace(UserAccessSecret)
			&& !string.IsNullOrWhiteSpace(ConsumerKey)
			&& !string.IsNullOrWhiteSpace(ConsumerSecret);
	}
}
