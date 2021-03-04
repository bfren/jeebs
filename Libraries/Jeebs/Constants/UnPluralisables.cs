using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
