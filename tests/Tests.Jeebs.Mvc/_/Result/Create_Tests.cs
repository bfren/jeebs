// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Models;
using MaybeF;

namespace Jeebs.Mvc.Result_Tests;
public class Create_Tests
{
	[Fact]
	public void With_Value__Sets_Value()
	{
		// Arrange
		var value = Rnd.Guid;

		// Act
		var r0 = Result.Create(value);
		var r1 = Result.Create(F.Some(value));

		// Assert
		Assert.Equal(value, r0.Value);
		Assert.Equal(value, r1.Value);
	}

	[Fact]
	public void With_Value_And_Message__Sets_Value_And_Message()
	{
		// Arrange
		var value = Rnd.Guid;
		var message = Alert.Warning(Rnd.Str);

		// Act
		var r0 = Result.Create(value, message);
		var r1 = Result.Create(F.Some(value), message);

		// Assert
		Assert.Equal(value, r0.Value);
		Assert.Equal(message, r0.Message);
		Assert.Equal(value, r1.Value);
		Assert.Equal(value, r1.Value);
		Assert.Equal(message, r1.Message);
	}
}
