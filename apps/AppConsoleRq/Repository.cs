// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Repository;
using Jeebs.Logging;

namespace AppConsoleRq;

internal sealed class TestRepository(IDb db, ILog<TestRepository> log) : Repository<TestEntity, TestId>(db, log);
