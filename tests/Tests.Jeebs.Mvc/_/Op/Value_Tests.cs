// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Op_Tests;

public class Value_Tests
{
	[Fact]
	public void Result_Is_Ok__Returns_Value()
	{
		// Arrange
		var value = Rnd.Guid;
		var maybe = R.Wrap(value);
		var jsonResult = new Op<Guid>(maybe);

		// Act
		var result = jsonResult.Value;

		// Assert
		Assert.Equal(value, result);
	}

	private void Result_Is_Fail__Returns_Null<T>()
	{
		// Arrange
		var failure = FailGen.Create();
		var jsonResult = new Op<T>(failure);

		// Act
		var result = jsonResult.Value;

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Nullable_Int__Returns_Null() =>
		Result_Is_Fail__Returns_Null<int?>();

	[Fact]
	public void Nullable_String__Returns_Null() =>
		Result_Is_Fail__Returns_Null<string?>();

	[Fact]
	public void Nullable_Guid__Returns_Null() =>
		Result_Is_Fail__Returns_Null<Guid?>();

	[Fact]
	public void Nullable_Class__Returns_Null() =>
		Result_Is_Fail__Returns_Null<Value_Tests?>();
}
