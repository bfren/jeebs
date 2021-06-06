// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Taxonomy
	/// </summary>
	public partial class QueryTaxonomy
	{
		/// <inheritdoc/>
		public sealed class Options : Data.Querying.QueryOptions
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
			/// No limit on taxonomy - return them all
			/// </summary>
			public Options() =>
				Limit = null;
		}
	}
}
