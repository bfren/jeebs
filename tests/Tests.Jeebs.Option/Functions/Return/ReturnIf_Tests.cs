// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class ReturnIf_Tests : Jeebs_Tests.ReturnIf_Tests
	{
		[Fact]
		public override void Test00_Exception_Thrown_By_Predicate_With_Value_Calls_Handler_Returns_None()
		{
			Test00((predicate, value, handler) => ReturnIf(predicate, value, handler));
		}

		[Fact]
		public override void Test01_Exception_Thrown_By_Predicate_With_Value_Func_Calls_Handler_Returns_None()
		{
			Test01((predicate, value, handler) => ReturnIf(predicate, value, handler));
		}

		[Fact]
		public override void Test02_Exception_Thrown_By_Value_Func_Calls_Handler_Returns_None()
		{
			Test02((predicate, value, handler) => ReturnIf(predicate, value, handler));
		}

		[Fact]
		public override void Test03_Predicate_True_With_Value_Returns_Some()
		{
			Test03((predicate, value, handler) => ReturnIf(predicate, value, handler));
		}

		[Fact]
		public override void Test04_Predicate_True_With_Value_Func_Returns_Some()
		{
			Test04((predicate, value, handler) => ReturnIf(predicate, value, handler));
		}

		[Fact]
		public override void Test05_Predicate_False_With_Value_Returns_None_With_PredicateWasFalseMsg()
		{
			Test05((predicate, value, handler) => ReturnIf(predicate, value, handler));
		}

		[Fact]
		public override void Test06_Predicate_False_With_Value_Func_Returns_None_With_PredicateWasFalseMsg()
		{
			Test06((predicate, value, handler) => ReturnIf(predicate, value, handler));
		}

		[Fact]
		public override void Test07_Predicate_False_Bypasses_Value_Func()
		{
			Test07((predicate, value, handler) => ReturnIf(predicate, value, handler));
		}
	}
}
