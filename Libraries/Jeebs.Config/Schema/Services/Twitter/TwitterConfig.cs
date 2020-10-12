using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Twitter configuration
	/// </summary>
	public class TwitterConfig : ServiceConfig
	{
		/// <summary>
		/// UserAccessToken
		/// </summary>
		public string UserAccessToken { get; set; } = string.Empty;

		/// <summary>
		/// UserAccessSecret
		/// </summary>
		public string UserAccessSecret { get; set; } = string.Empty;

		/// <summary>
		/// ConsumerKey
		/// </summary>
		public string ConsumerKey { get; set; } = string.Empty;

		/// <summary>
		/// ConsumerSecret
		/// </summary>
		public string ConsumerSecret { get; set; } = string.Empty;

		/// <inheritdoc/>
		public override bool IsValid
			=> !string.IsNullOrWhiteSpace(UserAccessToken)
			&& !string.IsNullOrWhiteSpace(UserAccessSecret)
			&& !string.IsNullOrWhiteSpace(ConsumerKey)
			&& !string.IsNullOrWhiteSpace(ConsumerSecret);
	}
}
