using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Posts Meta
	/// </summary>
	public partial class QueryPostsMeta
	{
		/// <summary>
		/// Query Options
		/// </summary>
		public sealed class Options : Data.QueryOptions
		{
			/// <summary>
			/// Search for multiple Posts
			/// </summary>
			public List<long>? PostIds { get; set; }

			/// <summary>
			/// Setup object
			/// </summary>
			public Options() => Limit = null;
		}
	}
}
