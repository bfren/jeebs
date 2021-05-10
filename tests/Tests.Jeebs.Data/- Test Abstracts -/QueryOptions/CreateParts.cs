// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class CreateParts<TOptions, TId> : QueryOptions_Tests<TOptions, TId>
		where TOptions : QueryOptions<TId>
		where TId : StrongId
	{
		public abstract void Test00_Returns_New_QueryParts_With_Table();

		protected void Test00()
		{
			// Arrange
			var (options, v) = Setup();
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(v.Table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Same(v.Table, some.From);
		}

		public abstract void Test01_Returns_New_QueryParts_With_Select();

		protected void Test01()
		{
			// Arrange
			var (options, v) = Setup();
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(v.Table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Same(cols, some.Select);
		}

		public abstract void Test02_Returns_New_QueryParts_With_Maximum();

		protected void Test02()
		{
			// Arrange
			var maximum = F.Rnd.Lng;
			var (options, v) = Setup(opt => opt with { Maximum = maximum });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(v.Table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Equal(maximum, some.Maximum);
		}

		public abstract void Test03_Returns_New_QueryParts_With_Skip();

		protected void Test03()
		{
			// Arrange
			var skip = F.Rnd.Lng;
			var (options, v) = Setup(opt => opt with { Skip = skip });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(v.Table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Equal(skip, some.Skip);
		}
	}
}
