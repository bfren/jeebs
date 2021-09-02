// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests;

public class Select_Tests
{
	[Fact]
	public void Selects_Matching_Columns_From_Tables()
	{
		// Arrange
		var table = new TestTable0();
		var builder = new QueryBuilderWithFrom(table);
		builder.Join<TestTable0, TestTable1>(QueryJoin.Inner, t => t.Foo, t => t.Bar);

		// Act
		var result = builder.Select<TestModel>();

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.Select,
			x =>
			{
				Assert.Equal("TestTable0", x.Table);
				Assert.Equal("TestFoo", x.Name);
				Assert.Equal("Foo", x.Alias);
			},
			x =>
			{
				Assert.Equal("TestTable1", x.Table);
				Assert.Equal("TestBar", x.Name);
				Assert.Equal("Bar", x.Alias);
			}
		);
	}
}

public sealed record class TestModel(int Foo, bool Bar);

public sealed record class TestTable0() : Table(nameof(TestTable0))
{
	public const string Prefix = "Test";

	public string Foo { get; set; } = Prefix + nameof(Foo);

	public string Ignore { get; set; } = Prefix + nameof(Ignore);
}

public sealed record class TestTable1() : Table(nameof(TestTable1))
{
	public const string Prefix = "Test";

	public string Bar { get; set; } = Prefix + nameof(Bar);
}
