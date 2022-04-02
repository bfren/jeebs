// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text;

namespace Jeebs.Functions;

public static partial class Base32F
{
	/// <summary>
	/// Convert <paramref name="bytes"/> to a Base32 string
	/// </summary>
	/// <param name="bytes">Input byte array</param>
	public static string ToBase32String(byte[] bytes)
	{
		// Check if byte array is null
		if (bytes.Length == 0)
		{
			return string.Empty;
		}

		// Prepare container for the final value
		var builder = new StringBuilder(bytes.Length * InByteSize / OutByteSize);

		// Position in the input buffer
		var bytesPosition = 0;

		// Offset inside a single byte that <bytesPosition> points to (from left to right)
		// 0 - highest bit, 7 - lowest bit
		var bytesSubPosition = 0;

		// Byte to look up in the dictionary
		var outputBase32Byte = 0;

		// The number of bits filled in the current output byte
		var outputBase32BytePosition = 0;

		// Iterate through input buffer until we reach past the end of it
		while (bytesPosition < bytes.Length)
		{
			// Calculate the number of bits we can extract out of current input byte to fill missing bits in the output byte
			var bitsAvailableInByte = Math.Min(InByteSize - bytesSubPosition, OutByteSize - outputBase32BytePosition);

			// Make space in the output byte
			outputBase32Byte <<= bitsAvailableInByte;

			// Extract the part of the input byte and move it to the output byte
			outputBase32Byte |= (byte)(bytes[bytesPosition] >> (InByteSize - (bytesSubPosition + bitsAvailableInByte)));

			// Update current sub-byte position
			bytesSubPosition += bitsAvailableInByte;

			// Check overflow
			if (bytesSubPosition >= InByteSize)
			{
				// Move to the next byte
				bytesPosition++;
				bytesSubPosition = 0;
			}

			// Update current base32 byte completion
			outputBase32BytePosition += bitsAvailableInByte;

			// Check overflow or end of input array
			if (outputBase32BytePosition >= OutByteSize)
			{
				// Drop the overflow bits
				outputBase32Byte &= 0x1F;  // 0x1F = 00011111 in binary

				// Add current Base32 byte and convert it to character
				_ = builder.Append(Base32Alphabet[outputBase32Byte]);

				// Move to the next byte
				outputBase32BytePosition = 0;
			}
		}

		// Check if we have a remainder
		if (outputBase32BytePosition > 0)
		{
			// Move to the right bits
			outputBase32Byte <<= OutByteSize - outputBase32BytePosition;

			// Drop the overflow bits
			outputBase32Byte &= 0x1F;  // 0x1F = 00011111 in binary

			// Add current Base32 byte and convert it to character
			_ = builder.Append(Base32Alphabet[outputBase32Byte]);
		}

		return builder.ToString();
	}
}
