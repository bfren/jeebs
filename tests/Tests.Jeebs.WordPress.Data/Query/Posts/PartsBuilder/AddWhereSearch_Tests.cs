// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests;

public class AddWhereSearch_Tests : QueryPartsBuilder_Tests<Query.PostsPartsBuilder, WpPostId>
{
	protected override Query.PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, Compare.Equal, F.Rnd.Str);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x => Assert.Collection(x.parameters,
				y => Assert.Equal("search", y.Key)
			)
		);
	}

	[Fact]
	public void Trims_Search_Text()
	{
		// Arrange
		var (builder, v) = Setup();
		var text = F.Rnd.Str;
		var textWithWhitespace = $"  {text}  ";

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, Compare.Equal, textWithWhitespace);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x => Assert.Collection(x.parameters,
				y => Assert.Equal(text, y.Value)
			)
		);
	}

	[Fact]
	public void Adds_Percent_To_Text_When_Compare_Like()
	{
		// Arrange
		var (builder, v) = Setup();
		var text = F.Rnd.Str;

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, Compare.Like, text);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x => Assert.Collection(x.parameters,
				y => Assert.Equal($"%{text}%", y.Value)
			)
		);
	}

	public static IEnumerable<object[]> Adds_SearchPostField_Data()
	{
		var post = new WpDbSchema(F.Rnd.Str).Post;

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
		var table = builder.TTest.Post.ToString();

		// Act
		var result = builder.AddWhereSearch(v.Parts, field, Compare.Equal, F.Rnd.Str);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x =>
			{
				Assert.Contains($"`{table}`.`{column}`", x.clause);
			}
		);
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
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.Title, cmp, F.Rnd.Str);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x =>
			{
				Assert.Contains($"{cmp.ToOperator()}", x.clause);
			}
		);
	}

	[Fact]
	public void Adds_All_SearchPostFields()
	{
		// Arrange
		var (builder, v) = Setup();
		var post = builder.TTest.Post;
		var table = post.ToString();

		// Act
		var result = builder.AddWhereSearch(v.Parts, SearchPostField.All, Compare.Equal, F.Rnd.Str);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x =>
			{
				Assert.Contains($"`{table}`.`{post.Title}`", x.clause);
				Assert.Contains($" OR `{table}`.`{post.Slug}`", x.clause);
				Assert.Contains($" OR `{table}`.`{post.Content}`", x.clause);
				Assert.Contains($" OR `{table}`.`{post.Excerpt}`", x.clause);
			}
		);
	}
}
