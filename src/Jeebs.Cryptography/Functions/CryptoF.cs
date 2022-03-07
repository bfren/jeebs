﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text;
using Jeebs;
using Sodium;
using Sodium.Exceptions;
using static F.MaybeF;

namespace F;

/// <summary>
/// Cryptography functions
/// </summary>
public static class CryptoF
{
	/// <summary>
	/// Calculate a 64-byte hash of a given input string
	/// </summary>
	/// <param name="input">Input string</param>
	public static Maybe<byte[]> Hash(string input) =>
		Hash(input, 64);

	/// <summary>
	/// Calculate a hash of a given input string
	/// </summary>
	/// <param name="input">Input string</param>
	/// <param name="bytes">The number of bytes for the hash - must be between 16 and 64</param>
	public static Maybe<byte[]> Hash(string input, int bytes) =>
		Some(
			() => Encoding.UTF8.GetBytes(input),
			e => new M.GettingBytesForGenericHashExceptionMsg(e)
		)
		.Map(
			x => GenericHash.Hash(x, null, bytes),
			e => e switch
			{
				BytesOutOfRangeException =>
					new M.HashBytesMustBeBetween16And64ExceptionMsg(e),

				_ =>
					new M.GenericHashExceptionMsg(e)
			}
		);

	/// <summary>
	/// Generate a 3-word passphrase
	/// </summary>
	public static Maybe<string> GeneratePassphrase() =>
		GeneratePassphrase(3);

	/// <summary>
	/// Generate a passphrase
	/// </summary>
	/// <param name="numberOfWords">The number of words in the passphrase (minimum: 2)</param>
	public static Maybe<string> GeneratePassphrase(int numberOfWords) =>
		Rnd.StringF.Passphrase(numberOfWords);

	/// <summary>
	/// Generate a 32 byte key to use for encryption
	/// </summary>
	public static Maybe<byte[]> GenerateKey() =>
		Some(
			() => SecretBox.GenerateKey(),
			e => new M.GeneratingKeyExceptionMsg(e)
		);

	/// <summary>
	/// Generate a 24 byte nonce to use for encryption
	/// </summary>
	public static byte[] GenerateNonce() =>
		SecretBox.GenerateNonce();

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Something went wrong while generating a key</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class GeneratingKeyExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Something went wrong while generating a nonce</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class GeneratingNonceExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>An unknown error occurred while creating the hash</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class GenericHashExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Error converting string to byte array</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class GettingBytesForGenericHashExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Hash bytes must be between 16 and 64</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class HashBytesMustBeBetween16And64ExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
