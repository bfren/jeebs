// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using System.Security.Cryptography;

namespace F
{
	public static partial class Rnd
	{
		/// <summary>
		/// Random Guid function
		/// </summary>
		public static class GuidF
		{
			/// <summary>
			/// Return a secure random Guid
			/// </summary>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
			public static Guid Get(RandomNumberGenerator? generator = null) =>
				new(ByteF.Get(16, generator));
		}
	}
}
