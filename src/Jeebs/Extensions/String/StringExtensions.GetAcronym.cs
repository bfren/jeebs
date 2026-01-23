// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Generate an acronym from a string.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns>Acronym.</returns>
	public static string GetAcronym(this string @this)
	{
		var acronym = new StringBuilder();

		for (var i = 0; i < @this.Length; i++)
		{
			if (char.IsUpper(@this[i]))
			{
				_ = acronym.Append(@this[i]);
			}
		}

		return acronym.ToString();
	}
}
