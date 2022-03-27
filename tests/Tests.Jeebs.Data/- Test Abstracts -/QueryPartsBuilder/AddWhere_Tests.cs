// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Id;
using static Jeebs.Reflection.LinqExpressionExtensions.M;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public abstract class AddWhere_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : class, IStrongId, new()
{
	public abstract void Test00_Column_Exists_Adds_Where();

	protected void Test00()
	{
		// Arrange
		var (builder, v) = Setup();
		var bar = Rnd.Str;
		var cmp = Compare.LessThan;
		var val = Rnd.Int;

		// Act
		var result = builder.AddWhere<TestTable>(v.Parts, new(Rnd.Str, bar), c => c.Bar, cmp, val);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(bar, x.column.ColName);
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
		var cmp = Compare.LessThan;
		var val = Rnd.Int;

		// Act
		var result = builder.AddWhere<TestTable>(v.Parts, new(Rnd.Str, Rnd.Str), _ => Rnd.Str, cmp, val);

		// Assert
		result.AssertNone().AssertType<PropertyDoesNotExistOnTypeMsg<TestTable>>();
	}

	public sealed record class TestTable(string Foo, string Bar) : Table(Rnd.Str);
}
