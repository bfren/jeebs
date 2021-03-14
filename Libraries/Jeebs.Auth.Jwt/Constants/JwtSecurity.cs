// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Constants
{
	/// <summary>
	/// Default JSON Web Token security algorithms and key lengths
	/// </summary>
	public static class JwtSecurity
	{
		/// <summary>
		/// Default signing algorithm (256-bits = 32 characters)
		/// </summary>
		public const string SigningAlgorithm = SecurityAlgorithms.HmacSha256Signature;

		/// <summary>
		/// Minimum length of signing key (in bytes)
		/// </summary>
		public const int SigningKeyBytes = 32;

		/// <summary>
		/// Default Key Wrap algorithm
		/// </summary>
		public const string KeyWrapAlgorithm = JwtConstants.DirectKeyUseAlg;

		/// <summary>
		/// Minimum length of signing key (in bytes)
		/// </summary>
		public const int EncryptingKeyBytes = 64;

		/// <summary>
		/// Default encrypting algorithm (512-bits = 64 characters)
		/// </summary>
		public const string EncryptingAlgorithm = SecurityAlgorithms.Aes256CbcHmacSha512;
	}
}
