// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Mapping;

public readonly record struct FooId(long Value) : IStrongId;
