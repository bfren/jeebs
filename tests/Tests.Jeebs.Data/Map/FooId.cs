// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Id;

namespace Jeebs.Data.Map;

public readonly record struct FooId(long Value) : IStrongId;
