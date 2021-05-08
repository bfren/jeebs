// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.WordPress.Data.Tables;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaOptions_Tests
{
	public class AddWhereKey_Tests
	{
		[Fact]
		public void Returns_New_Parts_With_Clause_And_Parameters()
		{
			// Arrange
			var v = PostsMetaOptions_Setup.Get();
			var keyColumn = new Column(v.Options.TableTest.GetName(), v.Options.TableTest.Key, nameof(PostMetaTable.Key));
			var key = F.Rnd.Str;
			var options = v.Options with { Key = key };

			// Act
			var result = options.AddWhereKey(v.Parts);

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
