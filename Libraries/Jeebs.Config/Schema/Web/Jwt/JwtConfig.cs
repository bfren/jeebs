using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		/// Signing key
		/// </summary>
		public string SigningKey { get; init; } = string.Empty;

		/// <summary>
		/// Encrypting key
		/// </summary>
		public string EncryptingKey { get; init; } = string.Empty;

		/// <summary>
		/// URI of application issuing this token
		/// </summary>
		public string Issuer { get; init; } = string.Empty;

		/// <summary>
		/// URI of application using this token
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
			&& !string.IsNullOrEmpty(Issuer);
	}
}
