// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="Db"/>
	/// <typeparam name="TDbClient">Database Client type</typeparam>
	public abstract class Db<TDbClient> : Db
		where TDbClient : IDbClient, new()
	{
		/// <summary>
		/// <typeparamref name="TDbClient"/>
		/// </summary>
		new protected TDbClient Client =>
			(TDbClient)base.Client;

		/// <inheritdoc cref="Db(IDbClient, DbLogs)"/>
		protected Db(DbLogs logs) : base(new TDbClient(), logs) { }

		/// <inheritdoc cref="Db(IDbClient, DbLogs, DbConfig)"/>
		protected Db(DbLogs logs, DbConfig config) : base(new TDbClient(), logs, config) { }

		/// <inheritdoc cref="Db(IDbClient, DbLogs, DbConfig, string)"/>
		protected Db(DbLogs logs, DbConfig config, string connectionName) : base(new TDbClient(), logs, config, connectionName) { }

		/// <inheritdoc cref="Db(IDbClient, DbLogs, string)"/>
		protected Db(DbLogs logs, string connectionString) : base(new TDbClient(), logs, connectionString) { }
	}
}
