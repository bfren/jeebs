// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities.WpTermTaxonomyEntityWithId_Tests;

public class Id_Tests : Id_Tests<Id_Tests.Test, Ids.WpTermTaxonomyId>
{
	[Fact]
	public override void Test00_Id_Returns_Database_Id() =>
		Test00(id => new() { Id = new() { Value = id } });

	public sealed record class Test : WpTermTaxonomyEntityWithId;
}
