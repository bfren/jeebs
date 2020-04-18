using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Taxonomy
	/// </summary>
	public partial class QueryTaxonomy
	{
		/// <summary>
		/// Query Options
		/// </summary>
		public sealed class Options : Data.QueryOptions
		{
			/// <summary>
			/// Search taxonomy type
			/// </summary>
			public Enums.Taxonomy? Taxonomy { get; set; }

			/// <summary>
			/// Search taxonomy term
			/// </summary>
			public string? Term { get; set; }

			/// <summary>
			/// Search taxonomy count (default: 1)
			/// (to override and show everything, set to zero)
			/// </summary>
			public long? CountAtLeast { get; set; } = 1;

			/// <summary>
			/// Remove LIMIT and OFFSET
			/// </summary>
			public Options()
			{
				Limit = null;
				Offset = null;
			}
		}
	}
}
