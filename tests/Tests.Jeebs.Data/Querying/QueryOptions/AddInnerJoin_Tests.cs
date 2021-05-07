// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddInnerJoin_Tests
	{
		[Fact]
		public void Adds_Columns_To_InnerJoin()
		{
			// Arrange
			var (_, _, _, parts, options) = QueryOptions_Setup.Get(opt => opt with { SortRandom = true });

			var t0Name = F.Rnd.Str;
			var t0Column = F.Rnd.Str;
			var t0 = new Table0(t0Name, t0Column);

			var t1Name = F.Rnd.Str;
			var t1Column = F.Rnd.Str;
			var t1 = new Table1(t1Name, t1Column);

			// Act
			var result = options.AddInnerJoinTest(parts, t0, t => t.Foo, t1, t => t.Bar);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.InnerJoin,
				x =>
				{
					Assert.Equal(t0Name, x.from.Table);
					Assert.Equal(t0Column, x.from.Name);
					Assert.Equal(nameof(Table0.Foo), x.from.Alias);

					Assert.Equal(t1Name, x.to.Table);
					Assert.Equal(t1Column, x.to.Name);
					Assert.Equal(nameof(Table1.Bar), x.to.Alias);
				}
			);
		}
	}
}
