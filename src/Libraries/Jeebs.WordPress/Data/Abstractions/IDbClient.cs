// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Database client
	/// </summary>
	public interface IDbClient
	{
		/// <summary>
		/// IAdapter
		/// </summary>
		public IAdapter Adapter { get; }

		/// <summary>
		/// Create IDbConnection using the specified connection string
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		/// <param name="encryptionKey">[Optional] Encryption key</param>
		/// <returns>IDbConnection</returns>
		IDbConnection Connect(string connectionString, string? encryptionKey = null);
	}
}
