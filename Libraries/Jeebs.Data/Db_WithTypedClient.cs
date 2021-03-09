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

		/// <inheritdoc cref="Db(IDbClient, ILog{Db}, ILog{UnitOfWork})"/>
		protected Db(ILog<Db> dbLog, ILog<UnitOfWork> wLog) : base(new TDbClient(), dbLog, wLog) { }

		/// <inheritdoc cref="Db(IDbClient, ILog{Db}, ILog{UnitOfWork}, DbConfig)"/>
		protected Db(ILog<Db> dbLog, ILog<UnitOfWork> wLog, DbConfig config) : base(new TDbClient(), dbLog, wLog, config) { }

		/// <inheritdoc cref="Db(IDbClient, ILog{Db}, ILog{UnitOfWork}, DbConfig, string)"/>
		protected Db(ILog<Db> dbLog, ILog<UnitOfWork> wLog, DbConfig config, string connectionName) : base(new TDbClient(), dbLog, wLog, config, connectionName) { }

		/// <inheritdoc cref="Db(IDbClient, ILog{Db}, ILog{UnitOfWork}, string)"/>
		protected Db(ILog<Db> dbLog, ILog<UnitOfWork> wLog, string connectionString) : base(new TDbClient(), dbLog, wLog, connectionString) { }
	}
}
