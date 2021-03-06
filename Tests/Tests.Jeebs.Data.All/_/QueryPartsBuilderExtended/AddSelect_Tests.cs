using NSubstitute;
using Xunit;
using static Jeebs.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.Data.QueryPartsBuilderExtended_Tests
{
	public class AddSelect_Tests
	{
		[Fact]
		public void Extracts_Escapes_Joins_Adds_To_Select()
		{
			// Arrange
			var (builder, adapter, table) = GetQueryPartsBuilder();
			adapter.ColumnSeparator
				.Returns(',');
			adapter.Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Is(table.ToString()))
				.Returns(x => $"[{x.ArgAt<string>(2)}].[{x.ArgAt<string>(0)}] AS '{x.ArgAt<string>(1)}'");

			// Act
			builder.AddSelect(table);

			// Assert
			adapter.Received(3).Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Is(table.ToString()));
			Assert.Equal(
				$"[{QueryPartsBuilderExtended.FooTable}].[{table.FooId}] AS '{nameof(table.FooId)}', " +
				$"[{QueryPartsBuilderExtended.FooTable}].[{table.Bar0}] AS '{nameof(table.Bar0)}', " +
				$"[{QueryPartsBuilderExtended.FooTable}].[{table.Bar1}] AS '{nameof(table.Bar1)}'",
				builder.Parts.Select
			);
		}
	}
}
