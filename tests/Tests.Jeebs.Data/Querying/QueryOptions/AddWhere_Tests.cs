// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;
using static Jeebs.Linq.LinqExpressionExtensions.Msg;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddWhere_Tests
	{
		[Fact]
		public void Column_Exists_Adds_Where()
		{
			// Arrange
			var (_, _, parts, options) = QueryOptions_Setup.Get();
			var bar = F.Rnd.Str;
			var cmp = Compare.LessThan;
			var val = F.Rnd.Int;

			// Act
			var result = options.AddWhereTest<TestTable>(parts, new(F.Rnd.Str, bar), c => c.Bar, cmp, val);

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

		[Fact]
		public void Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg()
		{
			// Arrange
			var (_, _, parts, options) = QueryOptions_Setup.Get();
			var bar = F.Rnd.Str;
			var cmp = Compare.LessThan;
			var val = F.Rnd.Int;

			// Act
			var result = options.AddWhereTest<TestTable>(parts, new(F.Rnd.Str, F.Rnd.Str), _ => F.Rnd.Str, cmp, val);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<PropertyDoesNotExistOnTypeMsg<TestTable>>(none);
		}

		public sealed record TestTable(string Foo, string Bar) : Table(F.Rnd.Str);
	}
}
