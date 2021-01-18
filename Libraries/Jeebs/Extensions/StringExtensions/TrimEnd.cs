﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Trim a string from the end of another string
		/// </summary>
		/// <param name="this">String object</param>
		/// <param name="value">Value to trim</param>
		/// <returns>String, with <paramref name="value"/> trimmed from the end</returns>
		public static string TrimEnd(this string @this, string value) =>
			@this.EndsWith(value) ? @this.Remove(@this.LastIndexOf(value, StringComparison.InvariantCulture)) : @this;
	}
}
