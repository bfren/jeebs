// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using static F.OptionF;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// String Extensions: Hash
	/// </summary>
	public static class StringExtensions_Hash
	{
		/// <summary>
		/// Compute a hash:<br/>
		/// A 32 byte hash returns a string of length 44<br/>
		/// A 64 byte hash (default) returns a string of length 88
		/// </summary>
		/// <param name="this">String to hash</param>
		/// <param name="bytes">Hash length in bytes - must be between 16 and 64</param>
		public static Option<string> Hash(this string @this, int bytes = 64) =>
			@this switch
			{
				string input when !string.IsNullOrWhiteSpace(input) =>
					F.CryptoF.Hash(@this, bytes)
						.Map(
							x => Convert.ToBase64String(x),
							DefaultHandler
						),

				_ =>
					string.Empty
			};
	}
}
