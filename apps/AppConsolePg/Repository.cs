// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Data;

namespace AppConsolePg;

internal class Repository : Repository<EntityTest, EntityTestId>
{
	public Repository(IDb db, ILog<Repository> log) : base(db, log) { }
}
