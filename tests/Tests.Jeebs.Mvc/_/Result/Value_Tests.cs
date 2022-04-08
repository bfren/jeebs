// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MaybeF;

namespace Jeebs.Mvc.Result_Tests;

public class Value_Tests
{
	[Fact]
	public void Maybe_Is_Some__Returns_Value()
	{
		// Arrange
		var value = Rnd.Guid;
		var maybe = F.Some(value);
		var jsonResult = new Result<Guid>(maybe);

		// Act
		var result = jsonResult.Value;

		// Assert
		Assert.Equal(value, result);
	}

	private void Maybe_Is_None__Returns_Null<T>()
	{
		// Arrange
		var maybe = Create.None<T>();
		var jsonResult = new Result<T>(maybe);

		// Act
		var result = jsonResult.Value;

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Nullable_Int__Returns_Null() =>
		Maybe_Is_None__Returns_Null<int?>();

	[Fact]
	public void Nullable_String__Returns_Null() =>
		Maybe_Is_None__Returns_Null<string?>();

	[Fact]
	public void Nullable_Guid__Returns_Null() =>
		Maybe_Is_None__Returns_Null<Guid?>();

	[Fact]
	public void Nullable_Class__Returns_Null() =>
		Maybe_Is_None__Returns_Null<Value_Tests?>();
}
