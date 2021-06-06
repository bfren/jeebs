// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;
using Xunit;
using static F.WordPressF.DataF.QueryPostsF;

namespace F.WordPressF.DataF.QueryPostsF_Tests
{
	public class GetTermLists_Tests
	{
		[Fact]
		public void No_TermList_Properties_Returns_Empty_List()
		{
			// Arrange

			// Act
			var result = GetTermLists<NoTermLists>();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void With_TermList_Properties_Returns_PropertyInfo()
		{
			// Arrange

			// Act
			var result = GetTermLists<WithTermLists>();

			// Assert
			Assert.Collection(result,
				x =>
				{
					Assert.Equal(nameof(WithTermLists.Terms0), x.Name);
					Assert.Equal(typeof(TermList), x.PropertyType);
				},
				x =>
				{
					Assert.Equal(nameof(WithTermLists.Terms1), x.Name);
					Assert.Equal(typeof(TermList), x.PropertyType);
				}
			);
		}

		public record NoTermLists;

		public record WithTermLists(TermList Terms0, TermList Terms1);
	}
}
