// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.Config
{
	/// <summary>
	/// Database connection configuration
	/// </summary>
	public readonly record struct DbConnectionConfig
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
