// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.OptionExtensions_Tests
{
	public class BindAsync_Tests : Jeebs_Tests.BindAsync_Tests
	{
		public override async Task Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			await Test00((opt, bind) => opt.AsTask.BindAsync(x => bind(x).GetAwaiter().GetResult()));
			await Test00((opt, bind) => opt.AsTask.BindAsync(bind));
		}

		public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
		{
			await Test01((opt, bind) => opt.AsTask.BindAsync(x => bind(x).GetAwaiter().GetResult()));
			await Test01((opt, bind) => opt.AsTask.BindAsync(bind));
		}

		public override async Task Test02_If_None_Gets_None()
		{
			await Test02((opt, bind) => opt.AsTask.BindAsync(x => bind(x).GetAwaiter().GetResult()));
			await Test02((opt, bind) => opt.AsTask.BindAsync(bind));
		}

		public override async Task Test03_If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			await Test03((opt, bind) => opt.AsTask.BindAsync(x => bind(x).GetAwaiter().GetResult()));
			await Test03((opt, bind) => opt.AsTask.BindAsync(bind));
		}

		public override async Task Test04_If_Some_Runs_Bind_Function()
		{
			await Test04((opt, bind) => opt.AsTask.BindAsync(x => bind(x).GetAwaiter().GetResult()));
			await Test04((opt, bind) => opt.AsTask.BindAsync(bind));
		}
	}
}
