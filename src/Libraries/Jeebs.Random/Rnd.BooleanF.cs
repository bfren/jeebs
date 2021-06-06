// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Security.Cryptography;

namespace F
{
	public static partial class Rnd
	{
		/// <summary>
		/// Boolean functions
		/// </summary>
		public static class BooleanF
		{
			/// <summary>
			/// Returns a random true or false value
			/// </summary>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
			public static bool FlipCoin(RandomNumberGenerator? generator = null) =>
				NumberF.GetInt64(0, 1, generator) switch
				{
					0 =>
						false,

					_ =>
						true
				};
		}
	}
}
