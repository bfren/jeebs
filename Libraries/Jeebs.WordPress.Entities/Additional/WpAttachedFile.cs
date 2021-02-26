using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.WordPress.Entities.Additional
{
	/// <summary>
	/// Attached file
	/// </summary>
	public abstract record WpAttachedFile
	{
		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Image URL
		/// </summary>
		public string Url { get; set; } = string.Empty;
	}
}
