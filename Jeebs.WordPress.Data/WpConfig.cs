using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// WordPress configuration
	/// </summary>
	public sealed class WpConfig
	{
		/// <summary>
		/// Database connection name (accessed via main Jeebs DB settings section)
		/// If empty, will attempt to use the default connection (as defined in main Jeebs DB settings section)
		/// </summary>
		public string Db { get; set; }

		/// <summary>
		/// If set, will override the table prefix in the DB connection settings
		/// </summary>
		public string TablePrefix { get; set; }

		/// <summary>
		/// /wp-content/uploads URL (for replacing with local URL)
		/// </summary>
		public string UploadsUrl { get; set; }

		/// <summary>
		/// /wp-content/uploads path (to access files directly)
		/// </summary>
		public string UploadsPath { get; set; }

		/// <summary>
		/// Files URL (alias to hide wp-content/uploads directory)
		/// </summary>
		public string VirtualUploadsUrl { get; set; }
	}
}
