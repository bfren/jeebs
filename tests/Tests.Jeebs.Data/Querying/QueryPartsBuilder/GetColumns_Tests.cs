// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class GetColumns_Tests : QueryPartsBuilder_Tests
	{
		[Fact]
		public void No_Matching_Properties_Returns_Empty_List()
		{
			// Arrange
			var (builder, _) = Setup();

			// Act
			var result = builder.GetColumns<TestEntity>();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Returns_Columns_For_Matching_Properties()
		{
			// Arrange
			var (builder, _) = Setup();
			var table = F.Rnd.Str;
			var column = F.Rnd.Str;
			builder.Table.Returns(new TestTable0(table, column));

			// Act
			var result = builder.GetColumns<TestModel>();

			// Assert
			Assert.Collection(result,
				x =>
				{
					Assert.Equal(table, x.Table);
					Assert.Equal(column, x.Name);
					Assert.Equal(nameof(TestTable0.Foo), x.Alias);
				}
			);
		}
	}
}
