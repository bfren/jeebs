// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Post Taxonomy
	/// </summary>
	internal partial class QueryPostsTaxonomy
	{
		/// <inheritdoc/>
		internal sealed class Options : Data.Querying.QueryOptions
		{
			/// <summary>
			/// The taxonomy to query
			/// </summary>
			public List<Taxonomy> Taxonomies { get; set; } = new List<Taxonomy>();

			/// <summary>
			/// Search for multiple Posts
			/// </summary>
			public List<long> PostIds { get; set; } = new List<long>();

			/// <summary>
			/// No limit on taxonomies - return them all
			/// </summary>
			public Options() =>
				Limit = null;
		}
	}
}
