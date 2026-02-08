// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Logging;
using Microsoft.Extensions.Options;
using Rqlite.Client;

namespace Jeebs.Data.Clients.Rqlite;

/// <inheritdoc/>
public sealed partial class RqliteDb : Db
{
	/// <inheritdoc/>
	internal IRqliteClientFactory Factory { get; private init; }

	/// <inheritdoc/>
	public RqliteDb(IRqliteClientFactory factory, IDbClient client, IOptions<DbConfig> config, ILog log, string name) :
		base(client, config, log, name) =>
		Factory = factory;
}
