// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Jeebs Authentication and Authorisation Configuraiton
	/// </summary>
	public record AuthConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = WebConfig.Key + ":auth";

		/// <summary>
		/// Whether or not auth is enabled
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Authentication scheme
		/// </summary>
		public AuthScheme? Scheme { get; set; }

		/// <summary>
		/// Path to the login page
		/// </summary>
		public string? LoginPath { get; set; }

		/// <summary>
		/// Path to the access denied page
		/// </summary>
		public string? AccessDeniedPath { get; set; }

		/// <summary>
		/// JwtConfig
		/// </summary>
		public JwtConfig Jwt { get; init; } = new();
	}
}
