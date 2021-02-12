using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Jeebs;

namespace F
{
	/// <summary>
	/// Random generator functions - very useful for testing
	/// </summary>
	public static class Rnd
	{
		/// <summary>
		/// Generate a random string 6 characters long, containing uppercase and lowercase letters but no numbers or special characters
		/// </summary>
		public static string Str =>
			StringF.Random(6);

		/// <summary>
		/// Generate a random 32-bit integer between 0 and 1000
		/// </summary>
		public static int Int =>
			MathsF.RandomInt32(max: 1000);

		/// <summary>
		/// Generate a random 64-bit integer between 0 and 1000
		/// </summary>
		public static long Lng =>
			MathsF.RandomInt64(max: 1000L);
	}
}
