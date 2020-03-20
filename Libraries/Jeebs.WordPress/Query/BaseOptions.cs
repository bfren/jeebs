using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.WordPress
{
	public static partial class Query
	{
		public class BaseOptions
		{
			/// <summary>
			/// Query Id
			/// </summary>
			public double? Id { get; set; }

			/// <summary>
			/// Query ORDER BY
			/// </summary>
			public (string selectColumn, SortOrder order)[]? Sort { get; set; }

			/// <summary>
			/// Set ORDER BY to random (will override Sort)
			/// </summary>
			public bool SortRandom { get; set; }

			/// <summary>
			/// Query LIMIT
			/// </summary>
			public double Limit { get; set; }

			/// <summary>
			/// Query OFFSET
			/// </summary>
			public double Offset { get; set; }

			/// <summary>
			/// Set defaults
			/// </summary>
			public BaseOptions()
			{
				Limit = 10;
			}
		}
	}
}