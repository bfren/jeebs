// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text;
using Jeebs.Messages;
using Maybe;
using Maybe.Functions;

namespace Jeebs.Functions;

/// <summary>
/// C# Base32 Converter
/// Copyright (c) 2014 Oleg Ignat - licensed under https://creativecommons.org/licenses/by/2.0/
/// See https://olegignat.com/base32/
/// Modifications to make consistent with Jeebs coding style and conventions (c) bfren as above
/// </summary>
public static class Base32F
{
	/// <summary>
	/// Size of the regular byte in bits
	/// </summary>
	private const int InByteSize = 8;

	/// <summary>
	/// Size of converted byte in bits
	/// </summary>
	private const int OutByteSize = 5;

	/// <summary>
	/// Alphabet - see https://datatracker.ietf.org/doc/html/rfc3548#section-5
	/// </summary>
	private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

	/// <summary>
	/// Convert byte array to Base32 format
	/// </summary>
	/// <param name="bytes">Byte array</param>
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
			return MaybeF.None<byte[], M.InputStringNotLongEnoughMsg>();
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
				return MaybeF.None<byte[]>(new M.CharacterNotInBase32AlphabetMsg(base32String[base32Position]));
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
	public static class M
	{
		/// <summary>Input string is not long enough to construct a complete byte array</summary>
		public sealed record class InputStringNotLongEnoughMsg : Msg;

		/// <summary>Input string contains a character that is not in the Base32 alphabet</summary>
		/// <param name="Value">Invalid character</param>
		public sealed record class CharacterNotInBase32AlphabetMsg(char Value) : WithValueMsg<char>;
	}
}
