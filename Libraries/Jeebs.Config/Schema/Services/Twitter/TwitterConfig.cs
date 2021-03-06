// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Config
{
	/// <summary>
	/// Twitter configuration
	/// </summary>
	public record TwitterConfig : ServiceConfig
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
		public override bool IsValid =>
			!string.IsNullOrWhiteSpace(UserAccessToken)
			&& !string.IsNullOrWhiteSpace(UserAccessSecret)
			&& !string.IsNullOrWhiteSpace(ConsumerKey)
			&& !string.IsNullOrWhiteSpace(ConsumerSecret);
	}
}
