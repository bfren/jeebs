// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;

namespace Jeebs.Functions;

public static partial class Base32F
{
	/// <summary>
	/// Convert base32 string to array of bytes
	/// </summary>
	/// <param name="base32String">Base32 string to convert - must be at least two characters</param>
	public static Maybe<byte[]> FromBase32String(string base32String)
	{
		// Check if string is empty
		if (string.IsNullOrEmpty(base32String))
		{
			return Array.Empty<byte>();
		}

		// Convert to upper-case
		var base32StringUpperCase = base32String.ToUpperInvariant();

		// Prepare output byte array
		var outputBytes = new byte[base32StringUpperCase.Length * OutByteSize / InByteSize];

		// Check the size
		if (outputBytes.Length == 0)
		{
			return F.None<byte[], M.InputStringNotLongEnoughMsg>();
		}

		// Position in the string
		var base32Position = 0;

		// Offset inside the character in the string
		var base32SubPosition = 0;

		// Position within outputBytes array
		var outputBytePosition = 0;

		// The number of bits filled in the current output byte
		var outputByteSubPosition = 0;

		// Normally we would iterate on the input array but in this case we actually iterate on the output array
		// We do it because output array doesn''t have overflow bits, while input does and it will cause output array overflow if we don''t stop in time
		while (outputBytePosition < outputBytes.Length)
		{
			// Look up current character in the dictionary to convert it to byte
			var currentBase32Byte = Base32Alphabet.IndexOf(base32StringUpperCase[base32Position]);

			// Check if found
			if (currentBase32Byte < 0)
			{
				return F.None<byte[]>(new M.CharacterNotInBase32AlphabetMsg(base32String[base32Position]));
			}

			// Calculate the number of bits we can extract out of current input character to fill missing bits in the output byte
			var bitsAvailableInByte = Math.Min(OutByteSize - base32SubPosition, InByteSize - outputByteSubPosition);

			// Make space in the output byte
			outputBytes[outputBytePosition] <<= bitsAvailableInByte;

			// Extract the part of the input character and move it to the output byte
			outputBytes[outputBytePosition] |= (byte)(currentBase32Byte >> (OutByteSize - (base32SubPosition + bitsAvailableInByte)));

			// Update current sub-byte position
			outputByteSubPosition += bitsAvailableInByte;

			// Check overflow
			if (outputByteSubPosition >= InByteSize)
			{
				// Move to the next byte
				outputBytePosition++;
				outputByteSubPosition = 0;
			}

			// Update current base32 byte completion
			base32SubPosition += bitsAvailableInByte;

			// Check overflow or end of input array
			if (base32SubPosition >= OutByteSize)
			{
				// Move to the next character
				base32Position++;
				base32SubPosition = 0;
			}
		}

		return outputBytes;
	}

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary>Input string is not long enough to construct a complete byte array</summary>
		public sealed record class InputStringNotLongEnoughMsg : Msg;

		/// <summary>Input string contains a character that is not in the Base32 alphabet</summary>
		/// <param name="Value">Invalid character</param>
		public sealed record class CharacterNotInBase32AlphabetMsg(char Value) : WithValueMsg<char>;
	}
}
