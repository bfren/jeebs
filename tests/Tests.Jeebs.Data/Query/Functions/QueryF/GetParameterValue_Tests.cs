// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Id;

namespace Jeebs.Data.Query.Functions.QueryF_Tests;

public class GetParameterValue_Tests
{
	[Fact]
	public void Not_StrongId_Returns_Original_Value()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = QueryF.GetParameterValue(value);

		// Assert
		Assert.Same(value, result);
	}

	[Fact]
	public void StrongId_Returns_StrongId_Value()
	{
		// Arrange
		var value = Rnd.Lng;

		// Act
		var result = QueryF.GetParameterValue(new TestId(value));

		// Assert
		Assert.Equal(value, result);
	}

	private readonly record struct TestId(long Value) : IStrongId;
}
