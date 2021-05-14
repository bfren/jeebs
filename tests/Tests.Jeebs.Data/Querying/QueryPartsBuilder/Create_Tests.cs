// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class Create_Tests : QueryPartsBuilder_Tests
	{
		[Fact]
		public void Returns_With_Table()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TestEntity>(null, F.Rnd.Lng);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Same(v.Table, some.From);
		}

		[Fact]
		public void Returns_With_Select()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TestEntity>(null, F.Rnd.Lng);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Same(v.Columns, some.Select);
		}

		[Fact]
		public void Returns_With_Maximum()
		{
			// Arrange
			var maximum = F.Rnd.Lng;
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TestEntity>(maximum, F.Rnd.Lng);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Equal(maximum, some.Maximum);
		}

		[Fact]
		public void Returns_With_Skip()
		{
			// Arrange
			var skip = F.Rnd.Lng;
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TestEntity>(null, skip);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Equal(skip, some.Skip);
		}
	}
}
