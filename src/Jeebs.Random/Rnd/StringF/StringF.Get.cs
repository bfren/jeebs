// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs;

namespace F;

public static partial class Rnd
{
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
			for (var i = 97; i <= 122; i++)
			{
				LowercaseChars.Add(Convert.ToChar(i));
			}

			UppercaseChars = new List<char>();
			for (var i = 65; i <= 90; i++)
			{
				UppercaseChars.Add(Convert.ToChar(i));
			}

			NumberChars = new List<char>();
			for (var i = 48; i <= 57; i++)
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
		/// Create a random string using default character groups - see <see cref="GetOptions.Default"/>
		/// </summary>
		/// <param name="length">The length of the new random string</param>
		public static string Get(int length) =>
			Get(length, GetOptions.Default);

		/// <summary>
		/// Create a random string using specified character groups
		/// Lowercase letters will always be used
		/// </summary>
		/// <param name="length">The length of the new random string</param>
		/// <param name="opt">GetOptions</param>
		public static string Get(int length, Func<GetOptions, GetOptions> opt) =>
			Get(length, opt(GetOptions.Default));

		/// <summary>
		/// Create a random string using specified character groups
		/// Lowercase letters will always be used
		/// </summary>
		/// <param name="length">The length of the new random string</param>
		/// <param name="options">GetOptions</param>
		/// <exception cref="InvalidOperationException"></exception>
		public static string Get(int length, GetOptions options)
		{
			// Setup
			var random = new List<char>();

			if (!options.IsValid)
			{
				throw new InvalidOperationException("You must include at least one character class.");
			}

			// Function to return a random list index
			void AppendOneOf(List<char> list)
			{
				var index = NumberF.GetInt32(max: list.Count - 1);
				random.Add(list[index]);
			}

			// Array of characters to use
			var chars = new List<char>();

			// Add lowercase characters
			if (options.Lower)
			{
				chars.AddRange(LowercaseChars);
				AppendOneOf(LowercaseChars);
			}

			// Add uppercase characters
			if (options.Upper)
			{
				chars.AddRange(UppercaseChars);
				AppendOneOf(UppercaseChars);
			}

			// Add numbers
			if (options.Numbers)
			{
				chars.AddRange(NumberChars);
				AppendOneOf(NumberChars);
			}

			// Add special characters
			if (options.Special)
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
			return new(random.ToArray().Shuffle());
		}

		/// <summary>
		/// Get Options - at least one character class must be included
		/// </summary>
		/// <param name="Lower">If true (default) lowercase letters will be included</param>
		/// <param name="Upper">If true (default) uppercase letters will be included</param>
		/// <param name="Numbers">If true numbers will be included</param>
		/// <param name="Special">If true special characters will be included</param>
		public sealed record class GetOptions(
			bool Lower,
			bool Upper,
			bool Numbers,
			bool Special
		)
		{
			/// <summary>
			/// Returns default character classes:
			///		<see cref="Lower"/> = true
			///		<see cref="Upper"/> = true
			///		<see cref="Numbers"/> = false
			///		<see cref="Special"/> = false
			/// </summary>
			internal static GetOptions Default =>
				new(true, true, false, false);

			/// <summary>
			/// Returns true if at least one character class is enabled
			/// </summary>
			internal bool IsValid =>
				Lower || Upper || Numbers || Special;
		};
	}
}
