﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Posts Meta
	/// </summary>
	public partial class QueryPostsMeta
	{
		/// <inheritdoc/>
		public sealed class Options : Data.Querying.QueryOptions
		{
			/// <summary>
			/// Search for a single Post
			/// </summary>
			public long? PostId { get; set; }

			/// <summary>
			/// Search for multiple Posts
			/// </summary>
			public List<long>? PostIds { get; set; }

			/// <summary>
			/// Only return a single Key
			/// </summary>
			public string? Key { get; set; }

			/// <summary>
			/// No limit on meta - return it all
			/// </summary>
			public Options()
				=> Limit = null;
		}
	}
}
