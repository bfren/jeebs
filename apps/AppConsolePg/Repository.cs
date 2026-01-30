// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Common;
using Jeebs.Logging;

namespace AppConsolePg;

internal sealed class Repository(IDb db, ILog<Repository> log) : Repository<TestEntity, TestId>(db, log);
