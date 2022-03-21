// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text;
using Jeebs.Messages;
using Sodium;
using Sodium.Exceptions;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
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
		F.Some(
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

	public static partial class M
	{
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
