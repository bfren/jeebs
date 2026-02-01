// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Common;

namespace Jeebs.WordPress.CustomFields.TextCustomField_Tests;

public class HydrateAsync_Tests
{
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Meta_Contains_Key_Sets_Value_Returns_True_Option(bool isRequired)
	{
		// Arrange
		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();
		var key = Rnd.Str;
		var value = Rnd.Str;
		var meta = new MetaDictionary { { key, value } };
		var field = new Test(key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, isRequired);

		// Assert
		result.AssertTrue();
		Assert.Equal(value, field.ValueObj);
	}

	[Fact]
	public async Task Meta_Does_Not_Contain_Key_IsRequired_True_Returns_None_With_MetaKeyNotFoundMsg()
	{
		// Arrange
		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();
		var meta = new MetaDictionary { { Rnd.Str, Rnd.Str } };
		var key = Rnd.Str;
		var field = new Test(key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, true);

		// Assert
		_ = result.AssertFailure("Meta Key '{Key}' not found for Custom Field '{Type}'.",
			key, nameof(Test)
		);
	}

	[Fact]
	public async Task Meta_Does_Not_Contain_Key_IsRequired_False_Returns_False_Option()
	{
		// Arrange
		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();
		var meta = new MetaDictionary { { Rnd.Str, Rnd.Str } };
		var field = new Test(Rnd.Str);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, false);

		// Assert
		result.AssertFalse();
		Assert.Equal(string.Empty, field.ValueObj);
	}

	public class Test(string key) : TextCustomField(key);
}
