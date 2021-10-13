// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Constants;

namespace F;

/// <summary>
/// JSON Web Tokens functions
/// </summary>
public static partial class JwtF
{
	private static string GenerateKey(int bytes) =>
		Rnd.StringF.Get(bytes, numbers: true, special: true);

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
