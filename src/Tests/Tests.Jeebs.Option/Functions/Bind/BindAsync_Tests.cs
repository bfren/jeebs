// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class BindAsync_Tests : Jeebs_Tests.BindAsync_Tests
	{
		public override async Task Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			await Test00((opt, bind) => BindAsync(opt, bind));
		}

		public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
		{
			await Test01((opt, bind) => BindAsync(opt, bind));
		}

		public override async Task Test02_If_None_Gets_None()
		{
			await Test02((opt, bind) => BindAsync(opt, bind));
		}

		public override async Task Test03_If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			await Test03((opt, bind) => BindAsync(opt, bind));
		}

		public override async Task Test04_If_Some_Runs_Bind_Function()
		{
			await Test04((opt, bind) => BindAsync(opt, bind));
		}
	}
}
