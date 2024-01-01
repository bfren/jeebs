// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class AddWhereSearch_Tests : QueryPartsBuilder_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void SearchText_Null_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, Compare.LessThanOrEqual, null);

		// Assert
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_Parameter_With_Name_Search()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, Compare.Equal, Rnd.Str);

		// Assert
		var some = result.AssertSome();
		var (_, parameters) = Assert.Single(some.WhereCustom);
		var single = Assert.Single(parameters);
		Assert.Equal("search", single.Key);
	}

	[Fact]
	public void Trims_Search_Text()
	{
		// Arrange
		var (builder, v) = Setup();
		var text = Rnd.Str;
		var textWithWhitespace = $"  {text}  ";

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, Compare.Equal, textWithWhitespace);

		// Assert
		var some = result.AssertSome();
		var (_, parameters) = Assert.Single(some.WhereCustom);
		var single = Assert.Single(parameters);
		Assert.Equal(text, single.Value);
	}

	[Fact]
	public void Adds_Percent_To_Text_When_Compare_Like()
	{
		// Arrange
		var (builder, v) = Setup();
		var text = Rnd.Str;

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, Compare.Like, text);

		// Assert
		var some = result.AssertSome();
		var (_, parameters) = Assert.Single(some.WhereCustom);
		var single = Assert.Single(parameters);
		Assert.Equal($"%{text}%", single.Value);
	}

	public static IEnumerable<object[]> Adds_SearchPostField_Data()
	{
		var post = new WpDbSchema(Rnd.Str).Posts;

		yield return new object[] { SearchPostField.Title, post.Title };
		yield return new object[] { SearchPostField.Slug, post.Slug };
		yield return new object[] { SearchPostField.Content, post.Content };
		yield return new object[] { SearchPostField.Excerpt, post.Excerpt };
	}

	[Theory]
	[MemberData(nameof(Adds_SearchPostField_Data))]
	public void Adds_SearchPostField(SearchPostField field, string column)
	{
		// Arrange
		var (builder, v) = Setup();
		var table = builder.TTest.Posts.ToString();

		// Act
		var result = builder.AddWhereSearch(v.Parts, field, Compare.Equal, Rnd.Str);

		// Assert
		var some = result.AssertSome();
		var (clause, _) = Assert.Single(some.WhereCustom);
		Assert.Contains($"`{table}`.`{column}`", clause);
	}

	public static IEnumerable<object[]> Adds_Comparison_Data() =>
		GetCompareValues();

	[Theory]
	[MemberData(nameof(Adds_Comparison_Data))]
	public void Adds_Comparison(Compare cmp)
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, cmp, Rnd.Str);

		// Assert
		var some = result.AssertSome();
		var (clause, _) = Assert.Single(some.WhereCustom);
		Assert.Contains($"{cmp.ToOperator()}", clause);
	}

	[Fact]
	public void Adds_All_SearchPostFields()
	{
		// Arrange
		var (builder, v) = Setup();
		var post = builder.TTest.Posts;
		var table = post.ToString();

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.All, Compare.Equal, Rnd.Str);

		// Assert
		var some = result.AssertSome();
		var (clause, _) = Assert.Single(some.WhereCustom);
		Assert.Contains($"`{table}`.`{post.Title}`", clause);
		Assert.Contains($" OR `{table}`.`{post.Slug}`", clause);
		Assert.Contains($" OR `{table}`.`{post.Content}`", clause);
		Assert.Contains($" OR `{table}`.`{post.Excerpt}`", clause);
	}
}
