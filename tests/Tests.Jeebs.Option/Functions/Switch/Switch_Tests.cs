// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class Switch_Tests : Jeebs_Tests.Switch_Tests
	{
		[Fact]
		public override void Test00_Return_Void_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg>>();
			Test00(opt => Switch(opt, some, none));
		}

		[Fact]
		public override void Test01_Return_Value_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg, string>>();
			Test01(opt => Switch(opt, some, none));
		}

		[Fact]
		public override void Test02_Return_Void_If_None_Runs_None_Action_With_Reason()
		{
			var some = Substitute.For<Action<int>>();
			Test02((opt, none) => Switch(opt, some, none));
		}

		[Fact]
		public override void Test03_Return_Value_If_None_Runs_None_Func_With_Reason()
		{
			var some = Substitute.For<Func<int, string>>();
			Test03((opt, none) => Switch(opt, some, none));
		}

		[Fact]
		public override void Test04_Return_Void_If_Some_Runs_Some_Action_With_Value()
		{
			var none = Substitute.For<Action<IMsg>>();
			Test04((opt, some) => Switch(opt, some, none));
		}

		[Fact]
		public override void Test05_Return_Value_If_Some_Runs_Some_Func_With_Value()
		{
			var none = Substitute.For<Func<IMsg, string>>();
			Test05((opt, some) => Switch(opt, some, none));
		}
	}
}
