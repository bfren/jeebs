// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Security.Cryptography;

namespace Jeebs.Random;

public static partial class Rnd
{
	public static partial class ByteF
	{
		/// <summary>
		/// Return an array of random bytes
		/// </summary>
		/// <param name="length">The length of the byte array</param>
		public static byte[] Get(int length)
		{
			Span<byte> b = stackalloc byte[length];
			RandomNumberGenerator.Fill(b);
			return b.ToArray();
		}
	}
}
