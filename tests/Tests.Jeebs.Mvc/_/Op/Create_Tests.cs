// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Models;

namespace Jeebs.Mvc.Op_Tests;

public class Create_Tests
{
	[Fact]
	public void With_Value__Sets_Value()
	{
		// Arrange
		var value = Rnd.Guid;

		// Act
		var r0 = Op.Create(value);
		var r1 = Op.Create(R.Wrap(value));

		// Assert
		var s0 = Assert.IsType<Op<Guid>>(r0);
		Assert.Equal(value, s0.Value);
		var s1 = Assert.IsType<Op<Guid>>(r1);
		Assert.Equal(value, s1.Value);
	}

	[Fact]
	public void With_Value_And_Message__Sets_Value_And_Message()
	{
		// Arrange
		var value = Rnd.Guid;
		var message = Alert.Warning(Rnd.Str);

		// Act
		var r0 = Op.Create(value, message);
		var r1 = Op.Create(R.Wrap(value), message);

		// Assert
		var s0 = Assert.IsType<Op<Guid>>(r0);
		Assert.Equal(value, s0.Value);
		Assert.Equal(message, s0.Message);
		var s1 = Assert.IsType<Op<Guid>>(r1);
		Assert.Equal(value, s1.Value);
		Assert.Equal(value, s1.Value);
		Assert.Equal(message, s1.Message);
	}
}
