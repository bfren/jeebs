// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddRightJoin_Tests : QueryPartsBuilder_Tests
	{
		[Fact]
		public void Adds_Columns_To_RightJoin()
		{
			// Arrange
			var (builder, v) = Setup();

			var t0Name = F.Rnd.Str;
			var t0Column = F.Rnd.Str;
			var t0 = new TestTable0(t0Name, t0Column);

			var t1Name = F.Rnd.Str;
			var t1Column = F.Rnd.Str;
			var t1 = new TestTable1(t1Name, t1Column);

			// Act
			var result = builder.AddRightJoin(v.Parts, t0, t => t.Foo, t1, t => t.Bar);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.RightJoin,
				x =>
				{
					Assert.Equal(t0Name, x.from.Table);
					Assert.Equal(t0Column, x.from.Name);

					Assert.Equal(t1Name, x.to.Table);
					Assert.Equal(t1Column, x.to.Name);
				}
			);
		}
	}
}
