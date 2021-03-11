﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.OptionSome_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Value()
		{
			// Arrange
			var value = F.Rnd.Str;

			// Act
			var result = new Some<string>(value);

			// Assert
			Assert.Equal(value, result.Value);
		}
	}
}
