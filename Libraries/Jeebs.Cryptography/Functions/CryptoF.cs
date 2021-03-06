// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Security.Cryptography;
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
		/// Calculate the MD5 hash of a given input string
		/// </summary>
		/// <param name="input">Input string</param>
		public static string Md5(string input)
		{
			var bytes = Encoding.UTF8.GetBytes(input);
			using var md5 = MD5.Create();
			var hash = md5.ComputeHash(bytes);
			return BitConverter.ToString(hash);
		}

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
