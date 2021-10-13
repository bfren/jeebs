// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Xunit;
using static Jeebs.Linq.LinqExpressionExtensions.Msg;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests;

public abstract class AddWhere_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : IStrongId
{
	public abstract void Test00_Column_Exists_Adds_Where();

	protected void Test00()
	{
		// Arrange
		var (builder, v) = Setup();
		var bar = F.Rnd.Str;
		var cmp = Compare.LessThan;
		var val = F.Rnd.Int;

		// Act
		var result = builder.AddWhere<TestTable>(v.Parts, new(F.Rnd.Str, bar), c => c.Bar, cmp, val);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(bar, x.column.Name);
				Assert.Equal(cmp, x.cmp);
				Assert.Equal(val, x.value);
			}
		);
	}

	public abstract void Test01_Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg();

	protected void Test01()
	{
		// Arrange
		var (builder, v) = Setup();
		var bar = F.Rnd.Str;
		var cmp = Compare.LessThan;
		var val = F.Rnd.Int;

		// Act
		var result = builder.AddWhere<TestTable>(v.Parts, new(F.Rnd.Str, F.Rnd.Str), _ => F.Rnd.Str, cmp, val);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<PropertyDoesNotExistOnTypeMsg<TestTable>>(none);
	}

	public sealed record class TestTable(string Foo, string Bar) : Table(F.Rnd.Str);
}
