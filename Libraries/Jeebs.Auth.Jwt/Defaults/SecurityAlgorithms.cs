// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Defaults
{
	/// <summary>
	/// Default Jwt algorithms
	/// </summary>
	public static class Algorithms
	{
		/// <summary>
		/// Default signing algorithm (256-bits = 32 characters)
		/// </summary>
		public const string Signing = SecurityAlgorithms.HmacSha256;

		/// <summary>
		/// Default encrypting algorithm (512-bits = 64 characters)
		/// </summary>
		public const string Encrypting = SecurityAlgorithms.Aes256CbcHmacSha512;
	}
}
