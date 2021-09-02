﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Config.ServicesConfig_Tests
{
	public class SplitDefinition_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("onetwothree")]
		[InlineData("one.two.three")]
		public void Invalid_Definitition_Throws_InvalidServiceDefinitionException(string definition)
		{
			// Arrange

			// Act
			void action() => ServicesConfig.SplitDefinition(definition);

			// Assert
			var ex = Assert.Throws<InvalidServiceDefinitionException>(action);
			Assert.Equal(string.Format(InvalidServiceDefinitionException.Format, definition), ex.Message);
		}

		[Fact]
		public void Returns_Split_Definition()
		{
			// Arrange
			var t0 = F.Rnd.Str;
			var n0 = F.Rnd.Str;
			var definition = $"{t0}.{n0}";

			// Act
			var (t1, n1) = ServicesConfig.SplitDefinition(definition);

			// Assert
			Assert.Equal(t0, t1);
			Assert.Equal(n0, n1);
		}
	}
}
