// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.Data.TypeHandlers.Guid_Tests
{
	public class Parse_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Null_Or_Empty_Returns_Empty(object input)
		{
			// Arrange
			var handler = new GuidTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Equal(Guid.Empty, result);
		}

		[Theory]
		[InlineData(42)]
		[InlineData(true)]
		[InlineData("something wrong")]
		public void Invalid_Guid_Throws_FormatException(object input)
		{
			// Arrange
			var handler = new GuidTypeHandler();

			// Act
			void action() => handler.Parse(input);

			// Assert
			Assert.Throws<FormatException>(action);
		}

		[Fact]
		public void Valid_Guid_String_Returns_Guid()
		{
			// Arrange
			var value = F.Rnd.Guid;
			var handler = new GuidTypeHandler();

			// Act
			var result = handler.Parse(value.ToString());

			// Assert
			Assert.Equal(value, result);
		}
	}
}
