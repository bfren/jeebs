// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Jeebs;

namespace F
{
	public static partial class Rnd
	{
		/// <summary>
		/// String functions
		/// </summary>
		public static partial class StringF
		{
			/// <summary>
			/// List of all characters
			/// </summary>
			public static List<char> AllChars { get; }

			/// <summary>
			/// List of lowercase characters
			/// </summary>
			public static List<char> LowercaseChars { get; }

			/// <summary>
			/// List of uppercase characters
			/// </summary>
			public static List<char> UppercaseChars { get; }

			/// <summary>
			/// List of numeric characters
			/// </summary>
			public static List<char> NumberChars { get; }

			/// <summary>
			/// List of special characters
			/// </summary>
			public static List<char> SpecialChars { get; }

			/// <summary>
			/// Fill character lists
			/// </summary>
			static StringF()
			{
				LowercaseChars = new List<char>();
				for (int i = 97; i <= 122; i++)
				{
					LowercaseChars.Add(Convert.ToChar(i));
				}

				UppercaseChars = new List<char>();
				for (int i = 65; i <= 90; i++)
				{
					UppercaseChars.Add(Convert.ToChar(i));
				}

				NumberChars = new List<char>();
				for (int i = 48; i <= 57; i++)
				{
					NumberChars.Add(Convert.ToChar(i));
				}

				// Don't include % so we don't confuse SQL databases
				SpecialChars = new List<char>(new[] { '!', '#', '@', '+', '-', '*', '^', '=', ':', ';', '£', '$', '~', '`', '¬' });

				AllChars = new List<char>();
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
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
			public static string Get(
				int length,
				bool upper = true,
				bool numbers = false,
				bool special = false,
				RandomNumberGenerator? generator = null
			)
			{
				// Setup
				var random = new List<char>();

				// Function to return a random list index
				void AppendOneOf(List<char> list)
				{
					var index = NumberF.GetInt32(max: list.Count - 1, generator: generator);
					random.Add(list[index]);
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
}
