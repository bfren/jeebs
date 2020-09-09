using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Posts Meta
	/// </summary>
	internal partial class QueryPostsMeta
	{
		/// <inheritdoc/>
		internal sealed class Options : Data.QueryOptions
		{
			/// <summary>
			/// Search for multiple Posts
			/// </summary>
			public List<long>? PostIds { get; set; }

			/// <summary>
			/// No limit on meta - return it all
			/// </summary>
			public Options()
				=> Limit = null;
		}
	}
}
