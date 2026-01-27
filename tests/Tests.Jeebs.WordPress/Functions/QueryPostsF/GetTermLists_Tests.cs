// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Functions.QueryPostsF_Tests;

public class GetTermLists_Tests
{
	[Fact]
	public void No_TermList_Properties_Returns_Empty_List()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetTermLists<NoTermLists>();

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void With_TermList_Properties_Returns_PropertyInfo()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetTermLists<WithTermLists>();

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

	public record class NoTermLists;

	public record class WithTermLists(TermList Terms0, TermList Terms1);
}
