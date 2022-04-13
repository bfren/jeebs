// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using StrongId;

namespace AppConsolePg;

internal record struct ParamTest(int Id, string Foo, string Bar);

internal sealed record class EntityTestId : LongId;

internal record class EntityTest : IWithId<EntityTestId>
{
	public EntityTestId Id { get; init; } = new();

	public ParamTest Value { get; init; }
}