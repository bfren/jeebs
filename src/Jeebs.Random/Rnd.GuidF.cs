// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
