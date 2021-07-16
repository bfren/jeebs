// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Generate an acronym from a string
		/// </summary>
		/// <param name="this">Input string</param>
		public static string GetAcronym(this string @this)
		{
			var acronym = string.Empty;

			for (int i = 0; i < @this.Length; i++)
			{
				if (char.IsUpper(@this[i]))
				{
					acronym += @this[i];
				}
			}

			return acronym;
		}
	}
}
