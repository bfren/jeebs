// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.ObjectExtensions_Tests;

public class Some_Tests : Jeebs_Tests.Some_Tests
{
	[Fact]
	public override void Test04_Null_Input_Value_Returns_None()
	{
		Test04(val => val.Some());
	}

	[Fact]
	public override void Test06_Nullable_Allow_Null_False_Null_Input_Value_Returns_None_With_AllowNullWasFalseMsg()
	{
		Test06((val, nullable) => val.Some(nullable));
	}

	[Fact]
	public override void Test08_Nullable_Allow_Null_True_Null_Input_Value_Returns_Some_With_Null_Value()
	{
		Test08((val, nullable) => val.Some(nullable));
	}

	[Fact]
	public override void Test10_Not_Null_Value_Returns_Some()
	{
		Test10(val => val.Some());
	}

	[Fact]
	public override void Test12_Nullable_Not_Null_Value_Returns_Some()
	{
		Test12((val, nullable) => val.Some(nullable));
	}

	#region Unused

	[Fact]
	public override void Test00_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg() { }

	[Fact]
	public override void Test01_Nullable_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg() { }

	[Fact]
	public override void Test02_Exception_Thrown_With_Handler_Returns_None_Calls_Handler() { }

	[Fact]
	public override void Test03_Nullable_Exception_Thrown_With_Handler_Returns_None_Calls_Handler() { }

	[Fact]
	public override void Test05_Null_Input_Func_Returns_None() { }

	[Fact]
	public override void Test07_Nullable_Allow_Null_False_Null_Input_Func_Returns_None_With_AllowNullWasFalseMsg() { }

	[Fact]
	public override void Test09_Nullable_Allow_Null_True_Null_Input_Func_Returns_Some_With_Null_Value() { }

	[Fact]
	public override void Test11_Not_Null_Func_Returns_Some() { }

	[Fact]
	public override void Test13_Nullable_Not_Null_Func_Returns_Some() { }

	#endregion
}
