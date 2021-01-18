using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Jeebs.Constants;
using Jeebs.Reflection;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Rules for converting singular words into plural words
		/// This comes from https://mattgrande.wordpress.com/2009/10/28/pluralization-helper-for-c/
		/// and https://github.com/mattgrande/Grande.Pluralizer/blob/master/Grande.Pluralization/Pluralizer.cs
		/// </summary>
		private static readonly IDictionary<string, string> pluralisations = new Dictionary<string, string>
		{
			{ "person", "people" },
			{ "ox$", "oxen" },
			{ "^criterion$", "criteria" },
			{ "child", "children" },
			{ "foot", "feet" },
			{ "tooth", "teeth" },
			{ "goose", "geese" },
			{ "(.*[^af])fe?$", "$1ves" },					// ie, wolf, wife, but not giraffe, gaffe, safe
			{ "(hu|talis|otto|Ger|Ro)man$", "$1mans" },		// Exceptions for man -> men
			{ "(.*)man$", "$1men" },
			{ "(.+[^aeiou])y$", "$1ies" },
			{ "(.+zz)$", "$1es" },							// Buzz -> Buzzes
			{ "(.+z)$", "$1zes" },							// Quiz -> Quizzes
			{ "([m|l])ouse$", "$1ice" },
			{ "(append|matr|ind)(e|i)x$", "$1ices" },		// ie, Matrix, Index
			{ "(octop|vir|radi|fung)us$", "$1i" },
			{ "(phyl|milleni|spectr)um$", "$1a" },
			{ "(cris|ax)is$", "$1es" },
			{ "(.+(s|x|sh|ch))$", "$1es" },
			{ "(.+)ies$", "$1ies" },
			{ "(.+)", "$1s" }
		};

		/// <summary>
		/// 'Pluralise' a string
		/// </summary>
		/// <param name="this">The string to pluralise</param>
		/// <param name="count">The number of items</param>
		/// <returns>Pluralised string</returns>
		public static string Pluralise(this string @this, long count)
		{
			if (count == 1)
			{
				return @this;
			}

			if (UnPluralisables.All.Contains(@this.ToLowerInvariant()))
			{
				return @this;
			}

			var plural = @this;
			foreach (var pluralisation in pluralisations)
			{
				if (Regex.IsMatch(@this, pluralisation.Key))
				{
					plural = Regex.Replace(@this, pluralisation.Key, pluralisation.Value);
					break;
				}
			}

			return plural;
		}
	}
}
