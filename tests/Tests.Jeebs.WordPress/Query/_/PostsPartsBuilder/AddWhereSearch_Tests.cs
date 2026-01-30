// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.QueryBuilder.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
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
		var ok = result.AssertOk();
		Assert.Equal(v.Parts, ok);
	}

	[Fact]
	public void Adds_Parameter_With_Name_Search()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, Compare.Equal, Rnd.Str);

		// Assert
		var ok = result.AssertOk();
		var (_, parameters) = Assert.Single(ok.WhereCustom);
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
		var ok = result.AssertOk();
		var (_, parameters) = Assert.Single(ok.WhereCustom);
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
		var ok = result.AssertOk();
		var (_, parameters) = Assert.Single(ok.WhereCustom);
		var single = Assert.Single(parameters);
		Assert.Equal($"%{text}%", single.Value);
	}

	public static TheoryData<SearchPostField, string> Adds_SearchPostField_Data()
	{
		var post = new WpDbSchema(Rnd.Str).Posts;

		return
		[
			(SearchPostField.Title, post.Title),
			(SearchPostField.Slug, post.Slug),
			(SearchPostField.Content, post.Content),
			(SearchPostField.Excerpt, post.Excerpt)
		];
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
		var ok = result.AssertOk();
		var (clause, _) = Assert.Single(ok.WhereCustom);
		Assert.Contains($"`{table}`.`{column}`", clause);
	}

	public static TheoryData<Compare> Adds_Comparison_Data() =>
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
		var ok = result.AssertOk();
		var (clause, _) = Assert.Single(ok.WhereCustom);
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
		var ok = result.AssertOk();
		var (clause, _) = Assert.Single(ok.WhereCustom);
		Assert.Contains($"`{table}`.`{post.Title}`", clause);
		Assert.Contains($" OR `{table}`.`{post.Slug}`", clause);
		Assert.Contains($" OR `{table}`.`{post.Content}`", clause);
		Assert.Contains($" OR `{table}`.`{post.Excerpt}`", clause);
	}
}
