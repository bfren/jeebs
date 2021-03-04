using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Configuration options for Web Apps
	/// </summary>
	public record WebConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":web";

		/// <summary>
		/// RedirectionsConfig
		/// </summary>
		public RedirectionsConfig Redirections { get; init; } = new();

		/// <summary>
		/// SiteVerificationConfig
		/// </summary>
		public VerificationConfig Verification { get; init; } = new();
	}
}
