// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class GetColumns_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
		where TBuilder : QueryPartsBuilder<TId>
		where TId : StrongId
	{
		public abstract void Test00_No_Matching_Properties_Returns_Empty_List();

		protected void Test00<TWithoutMatchinProperties>()
		{
			// Arrange
			var (builder, _) = Setup();

			// Act
			var result = builder.GetColumns<TWithoutMatchinProperties>();

			// Assert
			Assert.Empty(result);
		}

		public abstract void Test01_Returns_Columns_For_Matching_Properties();

		protected void Test01<TWithMatchingProperties>(ITable table, string column, string alias)
		{
			// Arrange
			var (builder, _) = Setup();
			builder.Table.Returns(table);

			// Act
			var result = builder.GetColumns<TWithMatchingProperties>();

			// Assert
			Assert.Collection(result,
				x =>
				{
					Assert.Equal(table.GetName(), x.Table);
					Assert.Equal(column, x.Name);
					Assert.Equal(alias, x.Alias);
				}
			);
		}
	}
}
