// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using StrongId;

namespace AppConsolePg;

public record struct ParamTest(int Id, string Foo, string Bar);

public sealed record class EntityTestId : LongId;

public sealed record class EntityTest : IWithId<EntityTestId>
{
	public EntityTestId Id { get; init; } = new();

	public ParamTest Value { get; init; }
}