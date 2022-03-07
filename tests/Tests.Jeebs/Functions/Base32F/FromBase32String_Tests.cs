// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Xunit;
using static F.Base32F;

namespace F.Base32F_Tests;

public class FromBase32String_Tests
{
	[Fact]
	public void Empty_String_Returns_Empty_Byte_Array()
	{
		// Arrange

		// Act
		var result = FromBase32String(string.Empty).UnsafeUnwrap();

		// Assert
		Assert.Empty(result);
	}

	[Theory]
	[InlineData("0")]
	public void Input_String_Too_Short_Returns_None_With_InputStringNotLongEnoughMsg(string input)
	{
		// Arrange

		// Act
		var result = FromBase32String(input);

		// Assert
		var msg = result.AssertNone();
		_ = Assert.IsType<M.InputStringNotLongEnoughMsg>(msg);
	}

	[Theory]
	[InlineData('0')]
	[InlineData('1')]
	[InlineData('8')]
	[InlineData('9')]
	public void Invalid_Character_In_Input_String_Returns_None_With_CharacterNotInBase32AlphabetMsg(char input)
	{
		// Arrange
		var str = Rnd.Str + input;

		// Act
		var result = FromBase32String(str);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<M.CharacterNotInBase32AlphabetMsg>(none);
		Assert.Equal(input, msg.Value);
	}

	[Fact]
	public void Returns_Byte_Array_Of_Correct_Length()
	{
		// Arrange
		var str = "5C5NHZDVBT4RWPBK";

		// Act
		var result = FromBase32String(str).UnsafeUnwrap();

		// Assert
		Assert.Equal(10, result.Length);
	}

	[Fact]
	public void Works_Both_Ways()
	{
		// Arrange
		var expected = FromBase32String("5C5NHZDVBT4RWPBK").UnsafeUnwrap();
		var str = ToBase32String(expected);

		// Act
		var result = FromBase32String(str).UnsafeUnwrap();

		// Assert
		Assert.Equal(expected, result);
	}
}
