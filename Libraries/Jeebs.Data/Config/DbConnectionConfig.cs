using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Database connection configuration
	/// </summary>
	public sealed class DbConnectionConfig
	{
		/// <summary>
		/// Database connection string
		/// </summary>
		public string ConnectionString { get; set; }

		/// <summary>
		/// Database table prefix
		/// </summary>
		public string TablePrefix { get; set; }

		/// <summary>
		/// Additional settings required for configuring this database connection
		/// </summary>
		public Dictionary<string, string> AdditionalSettings { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		public DbConnectionConfig()
		{
			ConnectionString = string.Empty;
			TablePrefix = string.Empty;
			AdditionalSettings = new Dictionary<string, string>();
		}
	}
}
