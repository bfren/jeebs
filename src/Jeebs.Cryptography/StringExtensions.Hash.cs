// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Cryptography.Functions;

namespace Jeebs.Cryptography;

public static partial class StringExtensions
{
	/// <summary>
	/// Compute a 64-byte hash, returning a string of length 88
	/// </summary>
	/// <param name="this">String to hash</param>
	public static Maybe<string> Hash(this string @this) =>
		Hash(@this, 64);

	/// <summary>
	/// Compute a hash:<br/>
	/// A 32 byte hash returns a string of length 44<br/>
	/// A 64 byte hash (default) returns a string of length 88
	/// </summary>
	/// <param name="this">String to hash</param>
	/// <param name="bytes">Hash length in bytes - must be between 16 and 64</param>
	public static Maybe<string> Hash(this string @this, int bytes) =>
		@this switch
		{
			string input when !string.IsNullOrWhiteSpace(input) =>
				CryptoF.Hash(@this, bytes)
					.Map(
						x => Convert.ToBase64String(x),
						F.DefaultHandler
					),

			_ =>
				string.Empty
		};
}
