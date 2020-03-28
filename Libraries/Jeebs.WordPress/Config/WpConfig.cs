using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// WordPress configuration
	/// </summary>
	public abstract class WpConfig
	{
		/// <summary>
		/// Path to WordPress settings configuration section
		/// </summary>
		public const string Key = ":wp";

		/// <summary>
		/// Database connection name (accessed via main Jeebs DB settings section)
		/// If empty, will attempt to use the default connection (as defined in main Jeebs DB settings section)
		/// </summary>
		public string Db { get; set; } = string.Empty;

		/// <summary>
		/// If set, will override the table prefix in the DB connection settings
		/// </summary>
		public string TablePrefix { get; set; } = string.Empty;

		/// <summary>
		/// /wp-content/uploads URL (for replacing with local URL)
		/// </summary>
		public string UploadsUrl { get; set; } = string.Empty;

		/// <summary>
		/// /wp-content/uploads path (to access files directly)
		/// </summary>
		public string UploadsPath { get; set; } = string.Empty;

		/// <summary>
		/// Files URL (alias to hide wp-content/uploads directory)
		/// </summary>
		public string VirtualUploadsUrl { get; set; } = string.Empty;
	}
}
