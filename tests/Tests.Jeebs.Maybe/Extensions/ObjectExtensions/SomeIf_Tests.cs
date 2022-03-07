// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.ObjectExtensions_Tests;

public class SomeIf_Tests : Jeebs_Tests.SomeIf_Tests
{
	[Fact]
	public override void Test00_Exception_Thrown_By_Predicate_With_Value_Calls_Handler_Returns_None()
	{
		Test00((predicate, value, handler) => value.SomeIf(predicate, handler));
		Test00((predicate, value, handler) => value.SomeIf(_ => predicate(), handler));
	}

	[Fact]
	public override void Test03_Predicate_True_With_Value_Returns_Some()
	{
		Test03((predicate, value, handler) => value.SomeIf(predicate, handler));
		Test03((predicate, value, handler) => value.SomeIf(_ => predicate(), handler));
	}

	[Fact]
	public override void Test05_Predicate_False_With_Value_Returns_None_With_PredicateWasFalseMsg()
	{
		Test05((predicate, value, handler) => value.SomeIf(predicate, handler));
		Test05((predicate, value, handler) => value.SomeIf(_ => predicate(), handler));
	}

	#region Unused

	[Fact]
	public override void Test01_Exception_Thrown_By_Predicate_With_Value_Func_Calls_Handler_Returns_None()
	{
		// Unused
	}

	[Fact]
	public override void Test02_Exception_Thrown_By_Value_Func_Calls_Handler_Returns_None()
	{
		// Unused
	}

	[Fact]
	public override void Test04_Predicate_True_With_Value_Func_Returns_Some()
	{
		// Unused
	}

	[Fact]
	public override void Test06_Predicate_False_With_Value_Func_Returns_None_With_PredicateWasFalseMsg()
	{
		// Unused
	}

	[Fact]
	public override void Test07_Predicate_False_Bypasses_Value_Func()
	{
		// Unused
	}

	#endregion Unused
}
