// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Jwt.Constants;

/// <summary>
/// Default JSON Web Token security algorithms and key lengths
/// </summary>
public static class JwtSecurity
{
	/// <summary>
	/// Default signing algorithm (256-bits = 32 characters)
	/// </summary>
	public static readonly string SigningAlgorithm = SecurityAlgorithms.HmacSha256Signature;

	/// <summary>
	/// Minimum length of signing key (in bytes)
	/// </summary>
	public static readonly int SigningKeyBytes = 32;

	/// <summary>
	/// Default Key Wrap algorithm
	/// </summary>
	public static readonly string KeyWrapAlgorithm = JwtConstants.DirectKeyUseAlg;

	/// <summary>
	/// Minimum length of signing key (in bytes)
	/// </summary>
	public static readonly int EncryptingKeyBytes = 64;

	/// <summary>
	/// Default encrypting algorithm (512-bits = 64 characters)
	/// </summary>
	public static readonly string EncryptingAlgorithm = SecurityAlgorithms.Aes256CbcHmacSha512;
}
