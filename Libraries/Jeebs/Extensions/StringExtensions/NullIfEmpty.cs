using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Jeebs.Reflection;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Return null if the string is empty or null - otherwise, return the string
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String object, or null</returns>
		public static string NullIfEmpty(this string @this) =>
			Modify(@this, () => @this);
	}
}
