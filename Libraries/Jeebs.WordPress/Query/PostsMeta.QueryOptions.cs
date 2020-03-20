using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Posts
	/// </summary>
	public partial class PostsMeta
	{
		/// <summary>
		/// Query Options
		/// </summary>
		public sealed class QueryOptions : Data.QueryOptions
		{
			/// <summary>
			/// Search for multiple Posts
			/// </summary>
			public List<long>? PostIds { get; set; }

			/// <summary>
			/// Setup object
			/// </summary>
			public QueryOptions() => Limit = null;
		}
	}
}
