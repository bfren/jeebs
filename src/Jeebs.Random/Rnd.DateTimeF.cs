// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Security.Cryptography;

namespace F
{
	public static partial class Rnd
	{
		/// <summary>
		/// Random DateTime function
		/// </summary>
		public static class DateTimeF
		{
			/// <summary>
			/// Return a random DateTime
			/// </summary>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
			public static DateTime Get(RandomNumberGenerator? generator = null) =>
				new(
					year: NumberF.GetInt32(1, 9999, generator),
					month: NumberF.GetInt32(1, 12, generator),
					day: NumberF.GetInt32(1, 28, generator),
					hour: NumberF.GetInt32(0, 23, generator),
					minute: NumberF.GetInt32(0, 59, generator),
					second: NumberF.GetInt32(0, 59, generator)
				);
		}
	}
}
