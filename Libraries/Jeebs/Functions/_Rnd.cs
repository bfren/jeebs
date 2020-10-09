using System;
using System.Collections.Generic;
using System.Text;

namespace F
{
	/// <summary>
	/// Random generator functions
	/// </summary>
	public static class Rnd
	{
		/// <summary>
		/// Generate a random string 6 characters long
		/// </summary>
		public static string String
			=> StringF.Random(6);

		/// <summary>
		/// Generate a random integer between 0 and 1000
		/// </summary>
		public static int Integer
			=> MathsF.RandomInt32(max: 1000);
	}
}
