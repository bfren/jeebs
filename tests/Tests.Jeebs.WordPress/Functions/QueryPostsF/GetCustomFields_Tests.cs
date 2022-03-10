// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.CustomFields;

namespace Jeebs.WordPress.Functions.QueryPostsF_Tests;

public class GetCustomFields_Tests
{
	[Fact]
	public void No_CustomFields_Returns_Empty_List()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetCustomFields<NoCustomFields>();

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void With_CustomFields_Returns_PropertyInfo()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetCustomFields<WithCustomFields>();

		// Assert
		Assert.Collection(result,
			x =>
			{
				Assert.Equal(nameof(WithCustomFields.Field0), x.Name);
				Assert.True(typeof(ICustomField).IsAssignableFrom(x.PropertyType));
			},
			x =>
			{
				Assert.Equal(nameof(WithCustomFields.Field1), x.Name);
				Assert.True(typeof(ICustomField).IsAssignableFrom(x.PropertyType));
			}
		);
	}

	public sealed record class NoCustomFields;

	public sealed record class WithCustomFields(CustomField0 Field0, AttachmentCustomField Field1);

	public sealed class CustomField0 : TextCustomField
	{
		public CustomField0() : base(Rnd.Str) { }
	}

	public sealed class CustomField1 : AttachmentCustomField
	{
		public CustomField1() : base(Rnd.Str) { }
	}
}
