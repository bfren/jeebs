// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Auth.Totp.Functions;

public static partial class TotpF
{
	/// <summary>
	/// Generate a time-based code from the specified key.
	/// </summary>
	/// <param name="key">Secret key</param>
	/// <param name="settings">TotpSettings</param>
	public static string GenerateCode(byte[] key, TotpSettings settings)
	{
		var counter = GetCurrentInterval(settings.PeriodSeconds);
		return GenerateCode(key, counter, settings.CodeLength);
	}

	/// <summary>
	/// Generate a counter-based code from the specified key and counter<br/>
	/// See https://stackoverflow.com/a/12398317/8199362
	/// </summary>
	/// <param name="key">Secret key</param>
	/// <param name="counter">Code counter</param>
	/// <param name="length">The length of the code to generate</param>
	internal static string GenerateCode(byte[] key, ulong counter, int length)
	{
		// Get counter bytes in big-endian order
		var counterBytes = BitConverter.GetBytes(counter);
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(counterBytes);
		}

		// Calculate the hash using specified key
		var hash = ComputeHash(key, counterBytes);

		// 32-bit integers require a byte array of length 4
		var selectedBytes = new byte[4];

		// Get required number of bytes in big-endian order
		Buffer.BlockCopy(hash, hash[^1] & 0xF, selectedBytes, 0, 4);
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(selectedBytes);
		}

		// Convert to 32-bit integer
		var selectedInteger = BitConverter.ToInt32(selectedBytes, 0);

		// Remove the most significant bit for interoperability
		var truncatedHash = selectedInteger & 0x7FFFFFFF;

		// Generate number of digits for given pin length
		var code = truncatedHash % (int)Math.Pow(10, length);

		// Pad start of number with zeroes
		return code.ToString(CultureInfo.InvariantCulture).PadLeft(length, '0');
	}
}
