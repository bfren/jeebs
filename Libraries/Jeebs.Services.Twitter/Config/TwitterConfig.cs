using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Services.Twitter
{
	/// <summary>
	/// Twitter Configuration
	/// </summary>
	public sealed class TwitterConfig
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
	}
}
