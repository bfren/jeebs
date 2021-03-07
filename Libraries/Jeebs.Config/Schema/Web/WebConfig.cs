// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		/// JwtConfig
		/// </summary>
		public JwtConfig Jwt { get; init; } = new();

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
