// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class Switch_Tests : Jeebs_Tests.Switch_Tests
	{
		[Fact]
		public override void Test00_Return_Void_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg>>();
			Test00(opt => opt.Switch(some, () => none(new TestMsg())));
			Test00(opt => opt.Switch(some, none));
		}

		[Fact]
		public override void Test01_Return_Value_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg, string>>();
			Test01(opt => opt.Switch(some, none(new TestMsg())));
			Test01(opt => opt.Switch(some, () => none(new TestMsg())));
			Test01(opt => opt.Switch(some, none));
		}

		[Fact]
		public override void Test02_Return_Void_If_None_Runs_None_Action_With_Reason()
		{
			var some = Substitute.For<Action<int>>();
			Test02((opt, none) => opt.Switch(some, () => none(new TestMsg())));
			Test02((opt, none) => opt.Switch(some, none));
		}

		[Fact]
		public override void Test03_Return_Value_If_None_Runs_None_Func_With_Reason()
		{
			var some = Substitute.For<Func<int, string>>();
			Test03((opt, none) => opt.Switch(some, none(new TestMsg())));
			Test03((opt, none) => opt.Switch(some, () => none(new TestMsg())));
			Test03((opt, none) => opt.Switch(some, none));
		}

		[Fact]
		public override void Test04_Return_Void_If_Some_Runs_Some_Action_With_Value()
		{
			Test04((opt, some) => opt.Switch(some, Substitute.For<Action>()));
			Test04((opt, some) => opt.Switch(some, Substitute.For<Action<IMsg>>()));
		}

		[Fact]
		public override void Test05_Return_Value_If_Some_Runs_Some_Func_With_Value()
		{
			var none = Substitute.For<Func<IMsg, string>>();
			Test05((opt, some) => opt.Switch(some, F.Rnd.Str));
			Test05((opt, some) => opt.Switch(some, Substitute.For<Func<string>>()));
			Test05((opt, some) => opt.Switch(some, Substitute.For<Func<IMsg, string>>()));
		}
	}
}
