// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Logging;

namespace AppConsolePg;

internal sealed class Repository : Repository<EntityTest, EntityTestId>
{
	public Repository(IDb db, ILog<Repository> log) : base(db, log) { }
}
