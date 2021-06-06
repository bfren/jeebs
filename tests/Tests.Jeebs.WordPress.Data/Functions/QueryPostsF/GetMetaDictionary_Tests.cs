// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Jeebs.WordPress.Data;
using Xunit;
using static F.WordPressF.DataF.QueryPostsF;

namespace F.WordPressF.DataF.QueryPostsF_Tests
{
	public class GetMetaDictionary_Tests
	{
		[Fact]
		public void No_MetaDictionary_Properties_Returns_None_With_MetaDictionaryPropertyNotFoundMsg()
		{
			// Arrange

			// Act
			var result = GetMetaDictionary<NoMetaDictionaryProperties>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<Msg.MetaDictionaryPropertyNotFoundMsg<NoMetaDictionaryProperties>>(none);
		}

		[Fact]
		public void More_Than_One_MetaDictionary_Properties_Returns_None_With_MoreThanOneMetaDictionaryMsg()
		{
			// Arrange

			// Act
			var result = GetMetaDictionary<MoreThanOneMetaDictionaryProperty>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<Msg.MoreThanOneMetaDictionaryMsg<MoreThanOneMetaDictionaryProperty>>(none);
		}

		[Fact]
		public void With_Single_MetaDictionary_Property_Returns_PropertyInfo()
		{
			// Arrange

			// Act
			var result = GetMetaDictionary<SingleMetaDictionaryProperty>();

			// Assert
			var some = result.AssertSome();
			Assert.Equal(nameof(SingleMetaDictionaryProperty.Meta), some.Name);
		}

		public sealed record NoMetaDictionaryProperties;

		public sealed record MoreThanOneMetaDictionaryProperty(MetaDictionary Meta0, MetaDictionary Meta1);

		public sealed record SingleMetaDictionaryProperty(MetaDictionary Meta);
	}
}
