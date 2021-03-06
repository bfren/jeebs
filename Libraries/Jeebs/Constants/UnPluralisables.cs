// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Collections.Generic;

namespace Jeebs.Constants
{
	/// <summary>
	/// Unpluralisable words
	/// </summary>
	public static class UnPluralisables
	{
		/// <summary>
		/// Return all unpluralisable words
		/// </summary>
		public static List<string> All =>
			new()
			{
				"aircraft",
				"deer",
				"equipment",
				"fish",
				"information",
				"money",
				"rice",
				"series",
				"sheep",
				"species",
				"swine",
				"trout",
			};
	}
}
