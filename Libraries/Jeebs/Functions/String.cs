using Jeebs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace F
{
	/// <summary>
	/// String functions
	/// </summary>
	public static class StringF
	{
		/// <summary>
		/// List of all characters
		/// </summary>
		public static List<char> AllChars = new List<char>();

		/// <summary>
		/// List of lowercase characters
		/// </summary>
		public static List<char> LowercaseChars = new List<char>();

		/// <summary>
		/// List of uppercase characters
		/// </summary>
		public static List<char> UppercaseChars = new List<char>();

		/// <summary>
		/// List of numeric characters
		/// </summary>
		public static List<char> NumberChars = new List<char>();

		/// <summary>
		/// List of special characters
		/// </summary>
		public static List<char> SpecialChars = new List<char>();

		/// <summary>
		/// Fill character lists
		/// </summary>
		static StringF()
		{
			for (int i = 97; i <= 122; i++)
			{
				LowercaseChars.Add(Convert.ToChar(i));
			}

			for (int i = 65; i <= 90; i++)
			{
				UppercaseChars.Add(Convert.ToChar(i));
			}

			for (int i = 48; i <= 57; i++)
			{
				NumberChars.Add(Convert.ToChar(i));
			}

			// Don't include % so we don't confuse SQL databases
			SpecialChars.AddRange(new[] { '!', '#', '@', '+', '-', '*', '^', '=', ':', ';', '£', '$', '~', '`', '¬' });

			AllChars.AddRange(LowercaseChars);
			AllChars.AddRange(UppercaseChars);
			AllChars.AddRange(NumberChars);
			AllChars.AddRange(SpecialChars);
		}

		/// <summary>
		/// Create a random string using specified character groups
		/// Lowercase letters will always be used
		/// </summary>
		/// <param name="length">The length of the new random string</param>
		/// <param name="upper">If true (default) uppercase letters will be included</param>
		/// <param name="numbers">If true numbers will be included</param>
		/// <param name="special">If true special characters will be included</param>
		/// <returns>Random string including specified character groups</returns>
		public static string Random(int length, bool upper = true, bool numbers = false, bool special = false)
		{
			// Setup
			var csp = new RNGCryptoServiceProvider();
			var random = new List<char>();

			// Function to return a random list index
			void AppendOneOf(List<char> list)
			{
				byte[] b = new byte[4];
				csp.GetBytes(b);

				var rnd = (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
				var idx = (int)Math.Round(rnd * ((list).Count - 1));

				random.Add(list[idx]);
			}

			// Array of characters to use
			var chars = new List<char>();

			// Add lowercase characters
			chars.AddRange(LowercaseChars);
			AppendOneOf(LowercaseChars);

			// Add uppercase characters
			if (upper)
			{
				chars.AddRange(UppercaseChars);
				AppendOneOf(UppercaseChars);
			}

			// Add numbers
			if (numbers)
			{
				chars.AddRange(NumberChars);
				AppendOneOf(NumberChars);
			}

			// Add special characters
			if (special)
			{
				chars.AddRange(SpecialChars);
				AppendOneOf(SpecialChars);
			}

			// If the array is now bigger than the requested length, throw an exception
			if (random.Count > length)
			{
				throw new InvalidOperationException("Using requested character groups results in a string longer than the one requested.");
			}

			// Generate the rest of the random characters
			while (random.Count < length)
			{
				AppendOneOf(chars);
			}

			// Return random string
			return new string(random.ToArray().Shuffle());
		}
	}
}
