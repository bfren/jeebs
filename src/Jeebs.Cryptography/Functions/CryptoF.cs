// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Text;
using Jeebs;
using Sodium;
using Sodium.Exceptions;
using static F.OptionF;

namespace F
{
	/// <summary>
	/// Cryptography functions
	/// </summary>
	public static class CryptoF
	{
		/// <summary>
		/// Calculate a hash of a given input string
		/// </summary>
		/// <param name="input">Input string</param>
		/// <param name="bytes">The number of bytes for the hash - must be between 16 and 64</param>
		public static Option<byte[]> Hash(string input, int bytes = 64) =>
			Return(
				() => Encoding.UTF8.GetBytes(input),
				e => new Msg.GettingBytesForGenericHashExceptionMsg(e)
			)
			.Map(
				x => GenericHash.Hash(x, null, bytes),
				e => e switch
				{
					BytesOutOfRangeException =>
						new Msg.HashBytesMustBeBetween16And64ExceptionMsg(e),

					_ =>
						new Msg.GenericHashExceptionMsg(e)
				}
			);

		/// <summary>
		/// Generate a passphrase
		/// </summary>
		/// <param name="numberOfWords">The number of words in the passphrase (minimum: 2)</param>
		public static Option<string> GeneratePassphrase(int numberOfWords = 3) =>
			Rnd.StringF.Passphrase(numberOfWords);

		/// <summary>
		/// Generate a 32 byte key to use for encryption
		/// </summary>
		/// <returns>32 byte key</returns>
		public static Option<byte[]> GenerateKey() =>
			Return(
				() => SecretBox.GenerateKey(),
				e => new Msg.GeneratingKeyExceptionMsg(e)
			);

		/// <summary>
		/// Generate a 24 byte nonce to use for encryption
		/// </summary>
		/// <returns>24 byte nonce</returns>
		public static byte[] GenerateNonce() =>
			SecretBox.GenerateNonce();

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Something went wrong while generating a key</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GeneratingKeyExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Something went wrong while generating a nonce</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GeneratingNonceExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>An unknown error occurred while creating the hash</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GenericHashExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error converting string to byte array</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GettingBytesForGenericHashExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Hash bytes must be between 16 and 64</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record HashBytesMustBeBetween16And64ExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
