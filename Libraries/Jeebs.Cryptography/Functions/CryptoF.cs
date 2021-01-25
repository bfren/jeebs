using System;
using System.Collections.Generic;
using System.Text;
using Sodium;

namespace F
{
	/// <summary>
	/// Cryptography functions
	/// </summary>
	public static class CryptoF
	{
		/// <summary>
		/// Generate a 32 byte key to use for encryption
		/// </summary>
		/// <returns>32 byte key</returns>
		public static byte[] GenerateKey() =>
			SecretBox.GenerateKey();

		/// <summary>
		/// Generate a 24 byte nonce to use for encryption
		/// </summary>
		/// <returns>24 byte nonce</returns>
		public static byte[] GenerateNonce() =>
			SecretBox.GenerateNonce();
	}
}
