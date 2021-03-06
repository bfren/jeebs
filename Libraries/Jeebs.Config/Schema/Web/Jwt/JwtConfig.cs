// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
		public const string Key = WebConfig.Key + ":jwt";

		/// <summary>
		/// Signing key (256-bit so must be at least 32 characters)
		/// </summary>
		public string SigningKey { get; init; } = string.Empty;

		/// <summary>
		/// Encrypting key (512-bit so must be 64 characters)
		/// </summary>
		public string EncryptingKey { get; init; } = string.Empty;

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
		public int ValidForHours { get; init; } = 12;

		/// <summary>
		/// Whether or not the configuration is valid
		/// </summary>
		public bool IsValid =>
			!string.IsNullOrEmpty(SigningKey)
			&& !string.IsNullOrEmpty(Issuer)
			&& !string.IsNullOrEmpty(Audience);
	}
}
