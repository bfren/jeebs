// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

var (_, log) = Jeebs.Apps.Host.Create(args);
log.Inf("Hello, world!");
log.Inf("Hello, {Name}!", "Ben");
