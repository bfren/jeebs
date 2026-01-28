// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class ByteExtensions
{
	/// <summary>
	/// Encode an input byte array as Base64.
	/// </summary>
	/// <param name="this">Input byte array.</param>
	/// <returns>Base64 encoded string.</returns>
	public static string Encode(this byte[] @this) =>
		@this switch
		{
			byte[] =>
				Convert.ToBase64String(@this),

			_ =>
				string.Empty
		};
}
