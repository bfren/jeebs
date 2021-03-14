// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Text.RegularExpressions;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Split a CamelCase string by capitals
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String split by capital letters</returns>
		public static string SplitByCapitals(this string @this) =>
			Modify(@this, () => Regex.Replace(@this, "( *)([A-Z])", " $2").Trim());
	}
}
