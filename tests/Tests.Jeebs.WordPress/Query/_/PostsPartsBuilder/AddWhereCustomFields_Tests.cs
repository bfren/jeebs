// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.CustomFields;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class AddWhereCustomFields_Tests : QueryPartsBuilder_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void No_CustomFields_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereCustomFields(v.Parts, Substitute.For<IImmutableList<(ICustomField, Compare, object)>>());

		// Assert
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	public static IEnumerable<object[]> Single_CustomField_Adds_Where_Clause_Data() =>
		GetCompareValues();

	[Theory]
	[MemberData(nameof(Single_CustomField_Adds_Where_Clause_Data))]
	public void Single_CustomField_Adds_Where_Clause(Compare input)
	{
		// Arrange
		var (builder, v) = Setup();

		var key = Rnd.Str;
		object value = Rnd.Str;

		var field = Substitute.For<ICustomField>();
		field.Key.Returns(key);

		var customFields = ImmutableList.Create((field, input, value));

		var p = builder.TTest.Posts.ToString();
		var pm = builder.TTest.PostsMeta.ToString();

		// Act
		var result = builder.AddWhereCustomFields(v.Parts, customFields);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x =>
			{
				Assert.Equal("(" +
					$"SELECT COUNT(1) FROM `{pm}` " +
					$"WHERE `{pm}`.`post_id` = `{p}`.`ID` " +
					$"AND `{pm}`.`meta_key` = @customField0_Key " +
					$"AND `{pm}`.`meta_value` {input.ToOperator()} @customField0_Value" +
					") = 1",
					x.clause
				);
			}
		);
	}

	[Fact]
	public void Single_CustomField_Adds_Parameters()
	{
		// Arrange
		var (builder, v) = Setup();

		var key = Rnd.Str;
		object value = Rnd.Str;

		var field = Substitute.For<ICustomField>();
		field.Key.Returns(key);

		var customFields = ImmutableList.Create((field, Compare.Equal, value));

		// Act
		var result = builder.AddWhereCustomFields(v.Parts, customFields);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x =>
			{
				Assert.Collection(x.parameters,
					y =>
					{
						Assert.Equal("@customField0_Key", y.Key);
						Assert.Equal(key, y.Value);
					},
					y =>
					{
						Assert.Equal("@customField0_Value", y.Key);
						Assert.Equal(value, y.Value);
					}
				);
			}
		);
	}

	[Fact]
	public void Multiple_CustomFields_Adds_Where_Clause()
	{
		// Arrange
		var (builder, v) = Setup();

		var k0 = Rnd.Str;
		object v0 = Rnd.Str;
		var f0 = Substitute.For<ICustomField>();
		f0.Key.Returns(k0);

		var k1 = Rnd.Str;
		object v1 = Rnd.Str;
		var f1 = Substitute.For<ICustomField>();
		f1.Key.Returns(k1);

		var customFields = ImmutableList.Create((f0, Compare.Equal, v0), (f0, Compare.Equal, v1));

		var p = builder.TTest.Posts.ToString();
		var pm = builder.TTest.PostsMeta.ToString();

		// Act
		var result = builder.AddWhereCustomFields(v.Parts, customFields);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x =>
			{
				Assert.Equal("(" +
					$"SELECT COUNT(1) FROM `{pm}` " +
					$"WHERE `{pm}`.`post_id` = `{p}`.`ID` " +
					$"AND `{pm}`.`meta_key` = @customField0_Key " +
					$"AND `{pm}`.`meta_value` = @customField0_Value" +
					") = 1 AND (" +
					$"SELECT COUNT(1) FROM `{pm}` " +
					$"WHERE `{pm}`.`post_id` = `{p}`.`ID` " +
					$"AND `{pm}`.`meta_key` = @customField1_Key " +
					$"AND `{pm}`.`meta_value` = @customField1_Value" +
					") = 1",
					x.clause
				);
			}
		);
	}

	[Fact]
	public void Multiple_CustomFields_Adds_Parameters()
	{
		// Arrange
		var (builder, v) = Setup();

		var k0 = Rnd.Str;
		object v0 = Rnd.Str;
		var f0 = Substitute.For<ICustomField>();
		f0.Key.Returns(k0);

		var k1 = Rnd.Str;
		object v1 = Rnd.Str;
		var f1 = Substitute.For<ICustomField>();
		f1.Key.Returns(k1);

		var customFields = ImmutableList.Create((f0, Compare.Equal, v0), (f1, Compare.Equal, v1));

		// Act
		var result = builder.AddWhereCustomFields(v.Parts, customFields);

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some.WhereCustom,
			x =>
			{
				Assert.Collection(x.parameters,
					y =>
					{
						Assert.Equal("@customField0_Key", y.Key);
						Assert.Equal(k0, y.Value);
					},
					y =>
					{
						Assert.Equal("@customField0_Value", y.Key);
						Assert.Equal(v0, y.Value);
					},
					y =>
					{
						Assert.Equal("@customField1_Key", y.Key);
						Assert.Equal(k1, y.Value);
					},
					y =>
					{
						Assert.Equal("@customField1_Value", y.Key);
						Assert.Equal(v1, y.Value);
					}
				);
			}
		);
	}
}
