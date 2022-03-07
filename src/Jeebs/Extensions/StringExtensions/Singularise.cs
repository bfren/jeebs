// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Text.RegularExpressions;
using Jeebs.Constants;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Rules for converting plural words into singular words
	/// This comes from http://lotsacode.wordpress.com/2010/03/05/singularization-pluralization-in-c/
	/// </summary>
	private static readonly IDictionary<string, string> singularisations = new Dictionary<string, string>
	{
		{ "people", "person" },
		{ "oxen", "ox" },
		{ "criteria", "criterion" },
		{ "children", "child" },
		{ "feet", "foot" },
		{ "teeth", "tooth" },
		{ "geese", "goose" },
		{ "(.*)ives?", "$1ife" },
		{ "(.*)ves?", "$1f" },
		{ "(.*)men$", "$1man" },
		{ "(.+[aeiou])ys$", "$1y" },
		{ "(.+[^aeiou])ies$", "$1y" },
		{ "(.+)zes$", "$1" },
		{ "([m|l])ice$", "$1ouse" },
		{ "matrices", "matrix" },
		{ "indices", "index" },
		{ "(.+[^aeiou])ices$","$1ice" },
		{ "(.*)ices", "$1ex" },
		{ "(octop|vir)i$", "$1us" },
		{ "(.+(s|x|sh|ch))es$", "$1" },
		{ "(.+)s", "$1" }
	};

	/// <summary>
	/// 'Singularise' a string
	/// </summary>
	/// <param name="this">The string to singularise</param>
	/// <returns>Singularised string</returns>
	public static string Singularise(this string @this)
	{
		if (UnPluralisables.All.Contains(@this.ToLowerInvariant()))
		{
			return @this;
		}

		var singular = @this;
		foreach (var item in singularisations)
		{
			if (Regex.IsMatch(@this, item.Key))
			{
				singular = Regex.Replace(@this, item.Key, item.Value);
				break;
			}
		}

		return singular;
	}
}
