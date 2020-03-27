﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Taxonomy
	/// </summary>
	public partial class Taxonomy
	{
		/// <summary>
		/// Query Options
		/// </summary>
		public sealed class QueryOptions : Data.QueryOptions
		{
			/// <summary>
			/// Search taxonomy type
			/// </summary>
			public Enums.Taxonomy Taxonomy { get; set; }

			/// <summary>
			/// Search taxonomy term
			/// </summary>
			public string Term { get; set; }

			/// <summary>
			/// Search taxonomy count (default: 1)
			/// (to override and show everything, set to zero)
			/// </summary>
			public int CountAtLeast { get; set; }

			/// <summary>
			/// By default only show taxonomy terms that have at least one post
			/// </summary>
			public QueryOptions()
			{
				Taxonomy = Enums.Taxonomy.Blank;
				Term = string.Empty;
				CountAtLeast = 1;
			}
		}
	}
}
