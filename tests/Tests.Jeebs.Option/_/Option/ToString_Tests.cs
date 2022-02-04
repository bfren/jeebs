// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests;

public class ToString_Tests
{
	[Fact]
	public void Some_With_Value_Returns_Value_ToString()
	{
		// Arrange
		var value = F.Rnd.Lng;
		var option = Some(value);

		// Act
		var result = option.ToString();

		// Assert
		Assert.Equal(value.ToString(), result);
	}

	[Fact]
	public void Some_Value_Is_Null_Returns_Type()
	{
		// Arrange
		int? value = null;
		var option = Some(value, true);

		// Act
		var result = option.ToString();

		// Assert
		Assert.Equal("Some: " + typeof(int?), result);
	}

	[Fact]
	public void None_Returns_Reason_ToString()
	{
		// Arrange
		var msg = new TestMsg();
		var option = None<int>(msg);

		// Act
		var result = option.ToString();

		// Assert
		Assert.Equal(typeof(TestMsg).ToString(), result);
	}

	[Fact]
	public void None_With_ExceptionMsg_Returns_Msg_Type_And_Exception_Message()
	{
		// Arrange
		var value = F.Rnd.Str;
		var exception = new Exception(value);
		var option = None<int, TestExceptionMsg>(exception);

		// Act
		var result = option.ToString();

		// Assert
		Assert.Equal($"{typeof(TestExceptionMsg)}: {value}", result);
	}

	public record class TestMsg : Msg;

	public record class TestExceptionMsg(Exception Value) : ExceptionMsg;
}
