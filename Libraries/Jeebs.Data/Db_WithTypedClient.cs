// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;

namespace Jeebs.Data
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

		/// <inheritdoc cref="Db(IDbClient, ILog)"/>
		protected Db(ILog log) : base(new TDbClient(), log) { }

		/// <inheritdoc cref="Db(IDbClient, ILog, DbConfig)"/>
		protected Db(ILog log, DbConfig config) : base(new TDbClient(), log, config) { }

		/// <inheritdoc cref="Db(IDbClient, ILog, DbConfig, string)"/>
		protected Db(ILog log, DbConfig config, string connectionName) : base(new TDbClient(), log, config, connectionName) { }

		/// <inheritdoc cref="Db(IDbClient, ILog, string)"/>
		protected Db(ILog log, string connectionString) : base(new TDbClient(), log, connectionString) { }
	}
}
