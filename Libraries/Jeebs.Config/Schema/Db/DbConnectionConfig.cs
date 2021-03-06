// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Collections.Generic;

namespace Jeebs.Config
{
	/// <summary>
	/// Database connection configuration
	/// </summary>
	public record DbConnectionConfig
	{
		/// <summary>
		/// Database connection string
		/// </summary>
		public string ConnectionString { get; init; } = string.Empty;

		/// <summary>
		/// Database table prefix
		/// </summary>
		public string TablePrefix { get; init; } = string.Empty;

		/// <summary>
		/// Additional settings required for configuring this database connection
		/// </summary>
		public Dictionary<string, string> AdditionalSettings { get; init; } = new();
	}
}
