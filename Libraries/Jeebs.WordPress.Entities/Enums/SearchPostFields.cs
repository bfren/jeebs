using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress.Enums
{
	/// <summary>
	/// Search Post Fields
	/// </summary>
	[Flags]
	public enum SearchPostFields
	{
		/// <summary>
		/// Search nothing
		/// </summary>
		None = 0,

		/// <summary>
		/// Search Title field
		/// </summary>
		Title = 1,

		/// <summary>
		/// Search Slug field
		/// </summary>
		Slug = 2,

		/// <summary>
		/// Search Content field
		/// </summary>
		Content = 4
	}
}
