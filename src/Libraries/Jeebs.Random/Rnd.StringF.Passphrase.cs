// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Jeebs;
using static F.OptionF;

namespace F
{
	public static partial class Rnd
	{
		public static partial class StringF
		{
			/// <summary>
			/// Lazy property to avoid multiple reflection calls
			/// </summary>
			private static readonly Lazy<string[]> wordList = new(
				() =>
				{
					// Attempt to get embedded word list file
					var wordListResource = typeof(Rnd).Assembly.GetManifestResourceStream("F.eff-long-word-list.csv");

					// If it exists, read all words into an array
					if (wordListResource is null)
					{
						return Array.Empty<string>();
					}

					// Read words into a list
					using var reader = new StreamReader(wordListResource);
					string? line;
					var words = new List<string>();
					while ((line = reader.ReadLine()) != null && !string.IsNullOrEmpty(line))
					{
						words.Add(line);
					}

					// Return as an array
					return words.ToArray();
				}
			);

			/// <summary>
			/// Generate a random passphrase
			/// </summary>
			/// <param name="numberOfWords">The number of words in the passphrase (minimum: 2)</param>
			/// <param name="separator">[Optional] Word separator</param>
			/// <param name="upperFirst">[Optional] Whether or not to make the first letter of each word upper case</param>
			/// <param name="includeNumber">[Optional] Whether or not to include a number with one of the words</param>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
			public static Option<string> Passphrase(
				int numberOfWords,
				char separator = '-',
				bool upperFirst = true,
				bool includeNumber = true,
				RandomNumberGenerator? generator = null
			) =>
				Passphrase(wordList.Value, numberOfWords, separator, upperFirst, includeNumber, generator);

			/// <inheritdoc cref="Passphrase(int, char, bool, bool, RandomNumberGenerator?)"/>
			/// <param name="wordList">List of words to use for the passphrase</param>
			internal static Option<string> Passphrase(
				string[] wordList,
				int numberOfWords,
				char separator = '-',
				bool upperFirst = true,
				bool includeNumber = true,
				RandomNumberGenerator? generator = null
			)
			{
				// Number of words must be at least 2
				if (numberOfWords < 2)
				{
					return None<string, Msg.NumberOfWordsMustBeAtLeastTwoMsg>();
				}

				// Get word list
				if (wordList.Length == 0)
				{
					return None<string, Msg.EmptyWordListMsg>();
				}

				// Number of words cannot be higher than the word list
				if (numberOfWords > wordList.Length)
				{
					return None<string, Msg.NumberOfWordsCannotBeMoreThanWordListMsg>();
				}

				// Get the right number of words
				var used = new List<int>();
				var words = new List<string>();
				for (int i = 0; i < numberOfWords; i++)
				{
					// Get a random word
					var index = getUniqueIndex();
					var word = wordList[index];

					// Make the first letter uppercase
					if (upperFirst)
					{
						word = word[0].ToString().ToUpper() + word[1..];
					}

					// Add a number to one of the words
					if (includeNumber && i == 0)
					{
						word += NumberF.GetInt64(0, 9);
					}

					// Add the word to the list
					words.Add(word);
				}

				// Shuffle the words
				var shuffled = words.ToArray().Shuffle();

				// Return joined
				return string.Join(separator, shuffled);

				// Get a unique word index
				int getUniqueIndex()
				{
					var index = 0;

					do
					{
						index = NumberF.GetInt32(0, wordList.Length - 1, generator);
					} while (used.Contains(index));

					used.Add(index);
					return index;
				}
			}

			/// <summary>Messages</summary>
			public static class Msg
			{
				/// <summary>Number of words must be at least 2</summary>
				public sealed record NumberOfWordsMustBeAtLeastTwoMsg : IMsg { }

				/// <summary>Number of words must be less than length of word list</summary>
				public sealed record NumberOfWordsCannotBeMoreThanWordListMsg : IMsg { }

				/// <summary>The word list was empty</summary>
				public sealed record EmptyWordListMsg : IMsg { }
			}
		}
	}
}
