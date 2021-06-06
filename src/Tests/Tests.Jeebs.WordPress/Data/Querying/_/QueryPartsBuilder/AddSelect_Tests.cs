// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static Jeebs.WordPress.Data.Querying.QueryPartsBuilder_Tests.QueryPartsBuilder;

namespace Jeebs.WordPress.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddSelect_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Parts_Select_Null_Or_Empty_Sets_Parts_Select(string input)
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			builder.Parts.Select = input;
			var select = F.Rnd.Str;

			// Act
			builder.AddSelect(select);

			// Assert
			Assert.Equal(select, builder.Parts.Select);
		}

		[Fact]
		public void Overwrite_True_Sets_Parts_Select_When()
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			builder.Parts.Select = F.Rnd.Str;
			var select = F.Rnd.Str;

			// Act
			builder.AddSelect(select, true);

			// Assert
			Assert.Equal(select, builder.Parts.Select);
		}

		[Fact]
		public void Parts_Select_Not_Null_Or_Empty_Overwrite_False_Throws_SelectAlreadySetException()
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			builder.Parts.Select = F.Rnd.Str;
			var select = F.Rnd.Str;

			// Act
			void action() => builder.AddSelect(select);

			// Assert
			Assert.Throws<Jx.Data.Querying.SelectAlreadySetException>(action);
		}
	}
}
