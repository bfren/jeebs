// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Enums;
using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.TaxonomyTypeHandler_Tests
{
	public class Parse_Tests
	{
		public static TheoryData<string, Taxonomy> Valid_Value_Returns_TaxonomyType_Data =>
			new()
			{
				{ string.Empty, Taxonomy.Blank },
				{ "category", Taxonomy.PostCategory },
				{ "link_category", Taxonomy.LinkCategory },
				{ "nav_menu", Taxonomy.NavMenu },
				{ "post_tag", Taxonomy.PostTag }
			};

		[Theory]
		[MemberData(nameof(Valid_Value_Returns_TaxonomyType_Data))]
		public void Valid_Value_Returns_TaxonomyType(string input, Taxonomy expected)
		{
			// Arrange
			var handler = new TaxonomyTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(expected, result);
		}

		[Theory]
		[InlineData(null)]
		public void Null_Value_Returns_Blank_Taxonomy(object input)
		{
			// Arrange
			var handler = new TaxonomyTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(Taxonomy.Blank, result);
		}

		[Fact]
		public void Invalid_Value_Returns_Blank_Taxonomy()
		{
			// Arrange
			var value = F.Rnd.Str;
			var handler = new TaxonomyTypeHandler();

			// Act
			var result = handler.Parse(value);

			// Assert
			Assert.Same(Taxonomy.Blank, result);
		}
	}
}
