// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.ObjectExtensions_Tests
{
	public class ReturnIf_Tests : Jeebs_Tests.ReturnIf_Tests
	{
		[Fact]
		public override void Test00_Exception_Thrown_By_Predicate_With_Value_Calls_Handler_Returns_None()
		{
			Test00((predicate, value, handler) => value.ReturnIf(predicate, handler));
			Test00((predicate, value, handler) => value.ReturnIf(_ => predicate(), handler));
		}

		[Fact]
		public override void Test03_Predicate_True_With_Value_Returns_Some()
		{
			Test03((predicate, value, handler) => value.ReturnIf(predicate, handler));
			Test03((predicate, value, handler) => value.ReturnIf(_ => predicate(), handler));
		}

		[Fact]
		public override void Test05_Predicate_False_With_Value_Returns_None_With_PredicateWasFalseMsg()
		{
			Test05((predicate, value, handler) => value.ReturnIf(predicate, handler));
			Test05((predicate, value, handler) => value.ReturnIf(_ => predicate(), handler));
		}

		#region Unused

		[Fact]
		public override void Test01_Exception_Thrown_By_Predicate_With_Value_Func_Calls_Handler_Returns_None() { }

		[Fact]
		public override void Test02_Exception_Thrown_By_Value_Func_Calls_Handler_Returns_None() { }

		[Fact]
		public override void Test04_Predicate_True_With_Value_Func_Returns_Some() { }

		[Fact]
		public override void Test06_Predicate_False_With_Value_Func_Returns_None_With_PredicateWasFalseMsg() { }

		[Fact]
		public override void Test07_Predicate_False_Bypasses_Value_Func() { }

		#endregion
	}
}
