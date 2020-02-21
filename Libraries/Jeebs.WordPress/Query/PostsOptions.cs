using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	public static partial class Query
	{
		public sealed class PostsOptions
		{
			public int? Id { get; set; }

			public string? SearchText { get; set; }
		}
	}
}
