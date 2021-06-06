// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// String Extensions
	/// </summary>
	public static partial class StringExtensions
	{
		/// <summary>
		/// Return empty if the input string is null or empty
		/// </summary>
		/// <param name="this">Input string</param>
		/// <param name="perform">Function to modify and return the input string</param>
		/// <param name="empty">[Optional] String to return if @this is empty</param>
		/// <returns>Modified input string</returns>
		private static string Modify(string @this, Func<string> perform, string? empty = null)
		{
			if (string.IsNullOrEmpty(@this))
			{
				return empty ?? @this;
			}

			return perform();
		}
	}
}
