// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class Bind_Tests : Jeebs_Tests.Bind_Tests
	{
		[Fact]
		public override void Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			Test00((opt, bind) => Bind(opt, bind));
		}

		[Fact]
		public override void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
		{
			Test01((opt, bind) => Bind(opt, bind));
		}

		[Fact]
		public override void Test02_If_None_Gets_None()
		{
			Test02((opt, bind) => Bind(opt, bind));
		}

		[Fact]
		public override void Test03_If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			Test03((opt, bind) => Bind(opt, bind));
		}

		[Fact]
		public override void Test04_If_Some_Runs_Bind_Function()
		{
			Test04((opt, bind) => Bind(opt, bind));
		}
	}
}
