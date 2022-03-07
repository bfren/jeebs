﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.IO;
using Jeebs;
using static F.MaybeF;

namespace F;

public static partial class Rnd
{
	public static partial class StringF
	{
		/// <summary>
		/// Lazy property so the resource is only loaded once
		/// </summary>
		private static readonly Lazy<string[]> wordList = new(
			() =>
			{
				// Attempt to get embedded word list file
				var wordListResource = typeof(Rnd).Assembly.GetManifestResourceStream("F.eff-long-word-list.txt");

				// Return empty array if the resource can't be found
				if (wordListResource is null)
				{
					return Array.Empty<string>();
				}

				// Read the words into a list
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

		/// <inheritdoc cref="Passphrase(string[], int, char, bool, bool)"/>
		public static Maybe<string> Passphrase(int numberOfWords) =>
			Passphrase(numberOfWords, '-', true, true);

		/// <inheritdoc cref="Passphrase(string[], int, char, bool, bool)"/>
		public static Maybe<string> Passphrase(
			int numberOfWords,
			char separator,
			bool upperFirst,
			bool includeNumber
		) =>
			Passphrase(wordList.Value, numberOfWords, separator, upperFirst, includeNumber);

		/// <summary>
		/// Generate a random passphrase
		/// </summary>
		/// <param name="wordList">List of words to use for the passphrase</param>
		/// <param name="numberOfWords">The number of words in the passphrase (minimum: 2)</param>
		/// <param name="separator">[Optional] Word separator</param>
		/// <param name="upperFirst">[Optional] Whether or not to make the first letter of each word upper case</param>
		/// <param name="includeNumber">[Optional] Whether or not to include a number with one of the words</param>
		internal static Maybe<string> Passphrase(
			string[] wordList,
			int numberOfWords,
			char separator = '-',
			bool upperFirst = true,
			bool includeNumber = true
		)
		{
			// Number of words must be at least 2
			if (numberOfWords < 2)
			{
				return None<string, M.NumberOfWordsMustBeAtLeastTwoMsg>();
			}

			// Get word list
			if (wordList.Length == 0)
			{
				return None<string, M.EmptyWordListMsg>();
			}

			// Number of words cannot be higher than the word list
			if (numberOfWords > wordList.Length)
			{
				return None<string, M.NumberOfWordsCannotBeMoreThanWordListMsg>();
			}

			// Get the right number of words
			var used = new List<int>();
			var words = new List<string>();
			for (var i = 0; i < numberOfWords; i++)
			{
				// Get the index of a word that hasn't been used yet
				var index = getUniqueIndex();
				used.Add(index);

				var word = wordList[index];

				// Make the first letter uppercase
				if (upperFirst)
				{
					word = word[0].ToString().ToUpperInvariant() + word[1..];
				}

				// Add a number to the first word (the list will be shuffled later)
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

			// Get a random array index that hasn't been used before
			int getUniqueIndex()
			{
				int index;
				do
				{
					index = NumberF.GetInt32(0, wordList.Length - 1);
				}
				while (used.Contains(index));

				return index;
			}
		}

		/// <summary>Messages</summary>
		public static class M
		{
			/// <summary>Number of words must be at least 2</summary>
			public sealed record class NumberOfWordsMustBeAtLeastTwoMsg : Msg;

			/// <summary>Number of words must be less than length of word list</summary>
			public sealed record class NumberOfWordsCannotBeMoreThanWordListMsg : Msg;

			/// <summary>The word list was empty</summary>
			public sealed record class EmptyWordListMsg : Msg;
		}
	}
}
