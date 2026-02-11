// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Logging;
using Microsoft.Extensions.Options;
using Rqlite.Client;

namespace Jeebs.Data.Clients.Rqlite;

/// <inheritdoc/>
public abstract partial class RqliteDb : Db
{
	/// <inheritdoc/>
	internal IRqliteClientFactory Factory { get; private init; }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="factory">IRqliteClientFactory.</param>
	/// <param name="client">IDbClient.</param>
	/// <param name="config">DbConfig options.</param>
	/// <param name="log">ILog.</param>
	/// <param name="name">DbConnectionConfig name.</param>
	public RqliteDb(IRqliteClientFactory factory, IDbClient client, IOptions<DbConfig> config, ILog log, string name) :
		base(client, config, log, name) =>
		Factory = factory;

	/// <summary>
	/// Start work using new RqliteClient object.
	/// </summary>
	/// <returns>New RqliteClient object.</returns>
	public IRqliteClient StartWork()
	{
		var name = Config.ConnectionString;
		return string.IsNullOrWhiteSpace(name) switch
		{
			true =>
				Factory.CreateClientWithDefaults(),

			false =>
				Factory.CreateClient(name)
		};
	}
}
