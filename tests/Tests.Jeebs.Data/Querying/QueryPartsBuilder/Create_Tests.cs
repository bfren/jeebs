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
			Assert.NotSame(v.Parts, result);
			Assert.Same(v.Table, result.From);
		}

		[Fact]
		public void Returns_With_Select()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TestEntity>(null, F.Rnd.Lng);

			// Assert
			Assert.NotSame(v.Parts, result);
			Assert.Same(v.Columns, result.Select);
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
			Assert.NotSame(v.Parts, result);
			Assert.Equal(maximum, result.Maximum);
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
			Assert.NotSame(v.Parts, result);
			Assert.Equal(skip, result.Skip);
		}
	}
}
