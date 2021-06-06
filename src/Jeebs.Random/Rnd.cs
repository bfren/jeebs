// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs;

namespace F
{
	/// <summary>
	/// Random generator functions - very useful for testing
	/// </summary>
	public static partial class Rnd
	{
		/// <summary>
		/// 'Flip a coin' - generate a random true / false
		/// </summary>
		public static bool Flip =>
			BooleanF.Get();

		/// <summary>
		/// Generate a random DateTime between the year 0 and the year 9999
		/// </summary>
		public static DateTime DateTime =>
			DateTimeF.Get();

		/// <summary>
		/// Generate a random string 6 characters long, containing uppercase and lowercase letters but no numbers or special characters
		/// </summary>
		public static string Str =>
			StringF.Get(6);

		/// <summary>
		/// Generate a random passphrase with five dictionary words, a number, and one uppercase letter
		/// </summary>
		public static Option<string> Pass =>
			StringF.Passphrase(5);

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
