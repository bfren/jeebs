// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Xunit;
using static Jeebs.Linq.LinqExpressionExtensions.Msg;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddWhere_Tests : QueryPartsBuilder_Tests
	{
		protected void Column_Exists_Adds_Where()
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

		protected void Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg()
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

		public sealed record TestTable(string Foo, string Bar) : Table(F.Rnd.Str);
	}
}
