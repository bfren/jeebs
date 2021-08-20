// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Configuration options for Web Apps
	/// </summary>
	public readonly record struct WebConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":web";

		/// <summary>
		/// Authentication and Authorisation configuration
		/// </summary>
		public AuthConfig Auth { get; init; } = new();

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
