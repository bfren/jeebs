// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Security.Cryptography;

namespace F
{
	/// <summary>
	/// Byte functions
	/// </summary>
	public static class ByteF
	{
		/// <summary>
		/// Return an array of random bytes
		/// </summary>
		/// <param name="length">The length of the byte array</param>
		/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
		public static byte[] Random(int length, RandomNumberGenerator? generator = null)
		{
			byte[] b = new byte[length];

			if (generator is null)
			{
				using var csp = new RNGCryptoServiceProvider();
				csp.GetBytes(b);
			}
			else
			{
				generator.GetBytes(b);
			}

			return b;
		}
	}
}
