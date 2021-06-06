// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace F
{
	/// <summary>
	/// Random generator functions - very useful for testing
	/// </summary>
	public static partial class Rnd
	{
		/// <summary>
		/// Generate a random string 6 characters long, containing uppercase and lowercase letters but no numbers or special characters
		/// </summary>
		public static string Str =>
			StringF.Get(6);

		/// <summary>
		/// Generate a random 32-bit integer between 0 and 10000
		/// </summary>
		public static int Int =>
			NumberF.GetInt32(max: 10000);

		/// <summary>
		/// Generate a random 64-bit integer between 0 and 10000
		/// </summary>
		public static long Lng =>
			NumberF.GetInt64(max: 10000L);

		/// <summary>
		/// Generate a random Guid
		/// </summary>
		public static Guid Guid =>
			GuidF.Get();
	}
}
