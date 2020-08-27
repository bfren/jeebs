using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Configuration options for Web Apps
	/// </summary>
	public sealed class WebConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":web";

		/// <summary>
		/// RedirectionsConfig
		/// </summary>
		public RedirectionsConfig Redirections { get; set; } = new RedirectionsConfig();

		/// <summary>
		/// SiteVerificationConfig
		/// </summary>
		public SiteVerificationConfig Verification { get; set; } = new SiteVerificationConfig();
	}
}
