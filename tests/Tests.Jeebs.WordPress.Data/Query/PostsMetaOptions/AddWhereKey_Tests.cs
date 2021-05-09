// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.WordPress.Data.Tables;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaOptions_Tests
{
	public class AddWhereKey_Tests : PostsMetaOptions_Tests
	{
		[Fact]
		public void Returns_New_Parts_With_Clause_And_Parameters()
		{
			// Arrange
			var (options, v) = Setup();
			var keyColumn = new Column(options.TableTest.GetName(), options.TableTest.Key, nameof(PostMetaTable.Key));
			var key = F.Rnd.Str;
			var optionsWithKey = options with { Key = key };

			// Act
			var result = optionsWithKey.AddWhereKey(v.Parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(keyColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(key, x.value);
				}
			);
		}
	}
}
