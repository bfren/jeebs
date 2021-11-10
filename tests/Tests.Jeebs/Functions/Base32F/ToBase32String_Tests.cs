// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Xunit;
using static F.Base32F;

namespace F.Base32F_Tests;

public class ToBase32String_Tests
{
	[Fact]
	public void Empty_Byte_Array_Returns_Empty_String()
	{
		// Arrange
		var bytes = Array.Empty<byte>();

		// Act
		var result = ToBase32String(bytes);

		// Assert
		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Returns_String_Of_Correct_Length()
	{
		// Arrange
		var bytes = Rnd.ByteF.Get(10);

		// Act
		var result = ToBase32String(bytes);

		// Assert
		Assert.Equal(16, result.Length);
	}

	[Fact]
	public void Works_Both_Ways()
	{
		// Arrange
		var expected = "5C5NHZDVBT4RWPBK";
		var bytes = FromBase32String(expected).UnsafeUnwrap();

		// Act
		var result = ToBase32String(bytes);

		// Assert
		Assert.Equal(expected, result);
	}
}
