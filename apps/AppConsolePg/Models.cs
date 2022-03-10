// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Id;

namespace AppConsolePg;

record struct ParamTest(int Id, string Foo, string Bar);

readonly record struct EntityTestId(long Value) : IStrongId;

record class EntityTest : IWithId<EntityTestId>
{
	[Id]
	public EntityTestId Id { get; init; } = new();

	public ParamTest Value { get; init; }
}