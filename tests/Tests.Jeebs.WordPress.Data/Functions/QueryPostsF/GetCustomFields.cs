// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;
using Xunit;
using static F.WordPressF.DataF.QueryPostsF;

namespace F.WordPressF.DataF.QueryPostsF_Tests
{
	public class GetCustomFields
	{
		[Fact]
		public void No_CustomFields_Returns_Empty_List()
		{
			// Arrange

			// Act
			var result = GetCustomFields<NoCustomFields>();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void With_CustomFields_Returns_PropertyInfo()
		{
			// Arrange

			// Act
			var result = GetCustomFields<WithCustomFields>();

			// Assert
			Assert.Collection(result,
				x =>
				{
					Assert.Equal(nameof(WithCustomFields.Field0), x.Name);
					Assert.True(x.PropertyType.IsAssignableFrom(typeof(ICustomField)));
				},
				x =>
				{
					Assert.Equal(nameof(WithCustomFields.Field1), x.Name);
					Assert.True(x.PropertyType.IsAssignableFrom(typeof(ICustomField)));
				}
			);
		}

		public sealed record NoCustomFields;

		public sealed record WithCustomFields(ICustomField Field0, ICustomField Field1);
	}
}
