// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using StrongId;

namespace Jeebs.Data.Testing.Query;

/// <summary>
/// <see cref="IFluentQuery{TEntity, TId}"/> unit test helper functions and assertions.
/// </summary>
public static partial class FluentQueryHelper
{
	/// <summary>Used in <see cref="IFake"/></summary>
	private sealed record class FakeId : LongId;

	/// <summary>Used in <see cref="IFake"/></summary>
	/// <param name="Id">ID</param>
	private sealed record class FakeEntity(FakeId Id) : IWithId<FakeId>;

	/// <summary>Used to get strongly-typed method names</summary>
	private interface IFake : IFluentQuery<FakeEntity, FakeId> { }
}
