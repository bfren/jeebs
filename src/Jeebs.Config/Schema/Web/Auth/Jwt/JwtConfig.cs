// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// JSON Web Tokens (JWT) configuration
	/// </summary>
	public record JwtConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = AuthConfig.Key + ":jwt";

		/// <summary>
		/// The generated signing key (key is rotated each time the application restarts)
		/// </summary>
		public string SigningKey { get; init; } = string.Empty;

		/// <summary>
		/// The generated signing key (key is rotated each time the application restarts)
		/// </summary>
		public string? EncryptingKey { get; init; }

		/// <summary>
		/// URL of application issuing this token
		/// </summary>
		public string Issuer { get; init; } = string.Empty;

		/// <summary>
		/// URL of application using this token
		/// </summary>
		public string Audience { get; init; } = string.Empty;

		/// <summary>
		/// Number of hours the token will be valid for
		/// </summary>
		public int ValidForHours { get; init; } = 1;

		/// <summary>
		/// Whether or not the configuration is valid
		/// </summary>
		public bool IsValid =>
			!string.IsNullOrEmpty(SigningKey)
			&& !string.IsNullOrEmpty(Issuer)
			&& !string.IsNullOrEmpty(Audience);
	}
}
