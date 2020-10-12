using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Database connection configuration
	/// </summary>
	public class DbConnectionConfig
	{
		/// <summary>
		/// Database connection string
		/// </summary>
		public string ConnectionString { get; set; } = string.Empty;

		/// <summary>
		/// Database table prefix
		/// </summary>
		public string TablePrefix { get; set; } = string.Empty;

		/// <summary>
		/// Additional settings required for configuring this database connection
		/// </summary>
		public Dictionary<string, string> AdditionalSettings { get; set; } = new Dictionary<string, string>();
	}
}
