// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Sodium;
using Sodium.Exceptions;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Calculate a 64-byte hash of a given input string.
	/// </summary>
	/// <param name="input">Input string.</param>
	/// <returns>64-byte hash of <paramref name="input"/>.</returns>
	public static Result<byte[]> Hash(string input) =>
		Hash(input, 64);

	/// <summary>
	/// Calculate a hash of a given input string.
	/// </summary>
	/// <param name="input">Input string.</param>
	/// <param name="bytes">The number of bytes for the hash - must be between 16 and 64.</param>
	/// <returns>Hash of <paramref name="input"/>.</returns>
	public static Result<byte[]> Hash(string input, int bytes) =>
		R
			.Try(
				() => Encoding.UTF8.GetBytes(input)
			)
			.Map(
				x => GenericHash.Hash(x, null, bytes),
				e => e switch
				{
					BytesOutOfRangeException =>
						R.Fail("Hash bytes must be between {Min} and {Max}.", 16, 64)
							.Ctx(nameof(CryptoF), nameof(Hash)),

					_ =>
						R.Fail(e)
				}
			);
}
