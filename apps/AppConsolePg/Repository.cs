// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Common;
using Jeebs.Logging;

namespace AppConsolePg;

internal sealed class JsonRepository(IDb db, ILog<JsonRepository> log) : Repository<JsonEntity, TestId>(db, log);
