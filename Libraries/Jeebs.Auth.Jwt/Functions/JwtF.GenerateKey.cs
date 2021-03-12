// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Constants;

namespace JeebsF
{
	/// <summary>
	/// JSON Web Tokens functions
	/// </summary>
	public static partial class JwtF
	{
		private static string GenerateKey(int bytes) =>
			StringF.Random(bytes, numbers: true, special: true);

		/// <summary>
		/// Generate a signing key of length <see cref="JwtSecurity.SigningKeyBytes"/>
		/// </summary>
		public static string GenerateSigningKey() =>
			GenerateKey(JwtSecurity.SigningKeyBytes);

		/// <summary>
		/// Generate an encrypting key of length <see cref="JwtSecurity.EncryptingKeyBytes"/>
		/// </summary>
		public static string GenerateEncryptingKey() =>
			GenerateKey(JwtSecurity.EncryptingKeyBytes);
	}
}
