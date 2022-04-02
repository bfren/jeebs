// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Services;

namespace Jeebs.Config.ServicesConfig_Tests;

public class SplitDefinition_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("one.two.three")]
	public void Invalid_Definitition_Throws_InvalidServiceDefinitionException(string definition)
	{
		// Arrange

		// Act
		var action = void () => ServicesConfig.SplitDefinition(definition);

		// Assert
		var ex = Assert.Throws<InvalidServiceDefinitionException>(action);
		Assert.Equal(string.Format(InvalidServiceDefinitionException.Format, definition), ex.Message);
	}

	[Fact]
	public void Returns_Split_Definition()
	{
		// Arrange
		var type = Rnd.Str;
		var name = Rnd.Str;
		var d0 = $"{type}.{name}";
		var d1 = $"{type}";

		// Act
		var (t0, n0) = ServicesConfig.SplitDefinition(d0);
		var (t1, n1) = ServicesConfig.SplitDefinition(d1);

		// Assert
		Assert.Equal(type, t0);
		Assert.Equal(name, n0);
		Assert.Equal(type, t1);
		Assert.Equal(string.Empty, n1);
	}
}
